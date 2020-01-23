#!/bin/sh

read -p '.PCD Filename without extension: ' filename

mkdir "${filename}"

cp "${filename}".pcd "${filename}/${filename}".pcd

./convert_pcd_ascii_binary "${filename}".pcd "${filename}/${filename}"_binary.pcd 1

./pcd2ply "${filename}/${filename}"_binary.pcd "${filename}/${filename}".ply