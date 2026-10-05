"""Rewrite the INSERT ... SELECT statements of a generated SP_EXPORT / SP_IMPORT body so
that every statement names its columns explicitly, taken from the live schema.
  export mode: all columns; identity columns are kept via SET IDENTITY_INSERT (staging is a copy)
  import mode: identity columns are dropped (head office assigns new ids), except the tables in
               KEEP_IDENTITY whose id is the key SP_DELETE1/SP_DELETE2 join on.
usage: fix-insert-lists.py <columns.tsv> <export|import> <file>   (rewrites the file in place)
"""
import re, sys
tsv, mode, path = sys.argv[1:4]
KEEP_IDENTITY = {'ownertransaction'}
cols, ident = {}, {}
for line in open(tsv, encoding='utf-8'):
    parts = line.rstrip('\n').split('\t')
    if len(parts) < 5: continue
    t, c, _, isid, iscomp = parts[:5]
    if iscomp.strip() == '1': continue
    cols.setdefault(t, []).append(c)
    if isid.strip() == '1': ident[t] = c
def bare(name): return re.sub(r'[\[\]]', '', name)
stmt = re.compile(r'(?P<ind>[ \t]*)INSERT\s+INTO\s+(?P<tgt>[\[\]\w]+\.[\[\]\w]+\.[\[\]\w]+)\s*(?P<list>\([^)]*\))?\s*SELECT\s+(?P<sel>.*?)\s+FROM\s+(?P<src>[\[\]\w]+\.[\[\]\w]+\.[\[\]\w]+)(?P<alias>\s+[A-Za-z])?(?P<where>\s+WHERE.*?)?\s*;', re.S | re.I)
report = []
def fix(m):
    tgt = m.group('tgt'); tbl = bare(tgt).split('.')[-1]
    if tbl not in cols:  # the schema has tables that differ only by case (TRACE_OtherIncome / Trace_OtherIncome), so match exactly first
        cand = [k for k in cols if k.lower() == tbl.lower()]
        if len(cand) != 1: raise SystemExit(f'unknown or ambiguous table {tgt}: {cand}')
        tbl = cand[0]
    allc = cols[tbl]; idc = ident.get(tbl)
    old = [bare(x.strip()) for x in m.group('list')[1:-1].split(',')] if m.group('list') else None
    if old:
        missing = [c for c in old if c.lower() not in {x.lower() for x in allc}]
        dropped = [c for c in allc if c.lower() not in {x.lower() for x in old}]
        if missing or dropped: report.append(f'{tbl}: list had unknown {missing}, omitted {dropped}')
    keep_id = (mode == 'export') or (tbl in KEEP_IDENTITY)
    use = allc if (keep_id or not idc) else [c for c in allc if c != idc]
    lst = ','.join(f'[{c}]' for c in use)
    ind = m.group('ind'); where = (m.group('where') or '').rstrip(); alias = m.group('alias') or ''
    body = f"{ind}INSERT INTO {tgt} ({lst})\n{ind}SELECT {lst}\n{ind}FROM {m.group('src')}{alias}{where};"
    if idc and keep_id:
        body = f"{ind}SET IDENTITY_INSERT {tgt} ON;\n{body}\n{ind}SET IDENTITY_INSERT {tgt} OFF;"
    return body
s = open(path, encoding='utf-8').read()
new, n = stmt.subn(fix, s)
open(path, 'w', encoding='utf-8', newline='\n').write(new)
print(f'{path}: {n} statements rewritten'); print('\n'.join(report))
