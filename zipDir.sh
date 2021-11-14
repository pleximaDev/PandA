#!/bin/bash
#chmod +x zipDirectory.sh

# using: ./zipDir.sh "./lab1" "output.zip"

echo "Zipping directory '$1'"
sudo zip -r $2 $1
