MCS=mcs
EXENAME=ServUOX
CURPATH=`pwd`
SRVPATH=${CURPATH}/Server
REFS=System.Drawing.dll
NOWARNS=0618,0219,0414,1635

PHONY : default build clean run

default: run

debug: 
	${MCS} -win32icon:${SRVPATH}/servuox.ico -r:${CURPATH}/Ultima.dll,${REFS} -nowarn:${NOWARNS} -target:exe -out:${CURPATH}/${EXENAME}.exe -d:DEBUG -d:MONO -d:ServUOX -d:NEWTIMERS -nologo -debug -unsafe -recurse:${SRVPATH}/*.cs
	sed -i.bak -e 's/<!--//g; s/-->//g' ${EXENAME}.exe.config

run: build
	${CURPATH}/${EXENAME}.sh

build: ${EXENAME}.sh

clean:
	rm -f ${EXENAME}.sh
	rm -f ${EXENAME}.exe
	rm -f ${EXENAME}.exe.mdb
	rm -f *.bin

${EXENAME}.sh: ${EXENAME}.exe
	echo "#!/bin/sh" > ${CURPATH}/${EXENAME}.sh
	echo "mono ${CURPATH}/${EXENAME}.exe" >> ${CURPATH}/${EXENAME}.sh
	chmod a+x ${CURPATH}/${EXENAME}.sh
	sed -i.bak -e 's/<!--//g; s/-->//g' ${EXENAME}.exe.config
