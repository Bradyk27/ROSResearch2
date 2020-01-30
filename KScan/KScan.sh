#!/bin/sh

read -p '.PCD Filename without extension: ' filename

mkdir "${filename}"

python3 PCDConverter.py "${filename}"

cp "${filename}".pcd "${filename}/${filename}.pcd"

mv "${filename}"_conv.pcd "${filename}/${filename}"_conv.pcd

./convert_pcd_ascii_binary "${filename}/${filename}"_conv.pcd "${filename}/${filename}"_binary.pcd 1

./pcd2ply "${filename}/${filename}"_binary.pcd "${filename}/${filename}".ply