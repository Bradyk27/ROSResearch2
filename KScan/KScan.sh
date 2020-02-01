#!/bin/sh

filename=$1

mkdir "${filename}"

if [ "$2" == '-s' ]
then
    python3 PCDConverter.py "${filename}"

    cp "${filename}".pcd "${filename}/${filename}.pcd"

    mv "${filename}"_conv.pcd "${filename}/${filename}"_conv.pcd

    ./convert_pcd_ascii_binary "${filename}/${filename}"_conv.pcd "${filename}/${filename}"_binary.pcd 1
    
    ./pcd2ply "${filename}/${filename}"_binary.pcd "${filename}/${filename}".ply

else
    cp "${filename}".pcd "${filename}/${filename}.pcd"

    ./convert_pcd_ascii_binary "${filename}/${filename}".pcd "${filename}/\
${filename}"_binary.pcd 1

    ./pcd2ply "${filename}/${filename}"_binary.pcd "${filename}/${filename}".pl\
y
fi

printf "\n[DONE] All files found in "${filename}"\n"