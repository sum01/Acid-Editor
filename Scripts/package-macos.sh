#!/usr/bin/env bash
cd ../

SCRIPTPATH="$( cd "$(dirname "$0")" ; pwd -P )"
APP=TestDeferred
BINPATH=${SCRIPTPATH}/Build/Debug/bin
BUNDLE=${BINPATH}/Acid-Editor.app

rm -rf ${BUNDLE}
mkdir ${BUNDLE}
mkdir ${BUNDLE}/Contents
mkdir ${BUNDLE}/Contents/MacOS
mkdir ${BUNDLE}/Contents/Resources
mkdir ${BUNDLE}/Contents/Frameworks

cp -r ${SCRIPTPATH}/Acid-Editor ${BUNDLE}/Contents/Resources
cp ${BINPATH}/Acid-Editor ${BUNDLE}/Contents/MacOS/Acid-Editor
cp ${SCRIPTPATH}/Scripts/Info.plist ${BUNDLE}/Contents/Info.plist