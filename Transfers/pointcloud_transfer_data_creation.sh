#!/bin/bash

# Assign the variables. #Obvioulsy variables need to be updated to refelct actual machine & folder. #also might consider only accepting pointcloud files instead of anything
USER='brady';
HOST='10.42.0.1';
LOCAL_PATH='~/Desktop/';
REMOTE_PATH='~/Desktop/';
TIME=2;

for f in {0..4} :
 do
  SIZE=$((10**f));
  ssh ${USER}@${HOST} "cd ${REMOTE_PATH}; truncate -s ${SIZE}M test_file.ply";
  for i in {1..5}
  do
  # Get the most recent file and assign it to `RECENT`.
  RECENT=$(ssh ${USER}@${HOST} ls -lrt ${REMOTE_PATH} | awk '/.*/ { f=$NF }; END { print f }');

  # Run the actual SCP command.
  START_TIME=`gdate +%s.%N`;
  scp ${USER}@${HOST}:${REMOTE_PATH}${RECENT} pointcloud.ply;
  END_TIME=`gdate +%s.%N`;
  RUN_TIME=$( echo "$END_TIME - $START_TIME" | bc -l );
  printf 'Runtime: %s\n' "${RUN_TIME} seconds";
  FILE_SIZE=$(stat -f%z pointcloud.ply);
  printf 'File size: %s\n' "${FILE_SIZE} bytes";
  BYTES_PER_SEC=$(echo "$FILE_SIZE / $RUN_TIME" | bc -l);
  printf 'Bytes / second: %s\n\n' "${BYTES_PER_SEC} b/s";
  echo -e "${FILE_SIZE},${BYTES_PER_SEC}" >> transfer_data_ply.csv
  sleep $TIME;
  # Calculate runtime and such
  done
 done