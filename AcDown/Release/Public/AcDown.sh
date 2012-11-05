#!/bin/sh

script=$(readlink -f "$0")
scriptdir=`dirname "$script"`
mono $scriptdir/AcDown.exe