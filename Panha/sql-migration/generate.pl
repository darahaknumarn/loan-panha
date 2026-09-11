use strict; use warnings;
# usage: generate.pl <srcdir> <outdir> <db> <LIVE> <STAGING> <proc>...
my ($src,$dst,$db,$LIVE,$STAGING,@procs) = @ARGV;
for my $p (@procs) {
  my $f = "$src/$db.$p.sql";
  open my $h,'<:raw',$f or die "$f: $!"; local $/; my $s = <$h>; close $h;

  # the shrink proc lives locally in each database, not in the staging database
  $s =~ s/\bEXEC\s+(?:\[)?TempData(?:\])?\s*\.\s*(?:\[)?dbo(?:\])?\s*\.\s*(\[?SP_SHRINK_TEMPDATA\]?)/EXEC dbo.$1/gi;
  # ...and it shrinks this client's staging database
  $s =~ s/SHRINKDATABASE\s*\(\s*N'TempData'\s*\)/SHRINKDATABASE(N'$STAGING')/gi;

  # staging qualifier, bracketed then bare (before the Data rule, so it cannot eat TempData)
  $s =~ s/\[TempData\]\s*\.\s*\[dbo\]\s*\./[$STAGING].[dbo]./gi;
  $s =~ s/\bTempData\s*\.\s*dbo\s*\./$STAGING.dbo./gi;
  # live qualifier
  $s =~ s/\[Data\]\s*\.\s*\[dbo\]\s*\./[$LIVE].[dbo]./gi;
  $s =~ s/(?<!Temp)\bData\s*\.\s*dbo\s*\./$LIVE.dbo./gi;

  die "$db.$p: leftover Data/TempData reference\n" if $s =~ /(?:\[)?\bTempData\b(?:\])?\s*\.|(?<!Temp)(?:\[)?\bData\b(?:\])?\s*\.\s*(?:\[)?dbo/i;

  $s =~ s/\bCREATE\s+PROCEDURE\b/ALTER PROCEDURE/i
    or die "$db.$p: no CREATE PROCEDURE header\n";

  $s =~ s/\A\s+//;
  open my $o,'>:raw',"$dst/$db.$p.sql" or die $!;
  print $o $s;
  close $o;
  print "  $db.$p\n";
}
