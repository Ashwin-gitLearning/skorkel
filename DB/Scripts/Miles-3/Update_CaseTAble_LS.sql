--select * from Scrl_CaseTbl
update Scrl_CaseTbl
set     strCitation = 'SKO' + substring(strCitation, 3, len(strCitation)-2)
where   strCitation like 'LS%'