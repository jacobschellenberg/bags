In Terry Goodkind's Sword of Truth series Zedd is often found saying "Bags!" Out of curiosity, I wrote this little program to tell me just how many times the word 'bags' was used.

# Bags
Reads in text files, spits out a json of all words and the number of times they are used.

# Usage
* Requires a folder called 'queue' and 'processed' in the same directory where the .exe is located.
* Put .txt files in the 'queue' folder. When the program is run, the processed .json files will appear in the 'processed' folder.
* If you have multiple .txt files to be processed, the program will also generate a 'total' file that is the accumulation of words used throughout all the given files.

# Stop Words
If you want to exclude certain words from being counted, add a 'StopWords.txt' that is new line deliminated. Each new line should contain the single word that will be excluded from being counted.

# Config
Add a 'config.json' to the same folder the .exe is located to provide extra options. See the /Defaults/config.json for available options.
