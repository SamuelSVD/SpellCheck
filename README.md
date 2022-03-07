[![GitHub release](https://img.shields.io/github/release/SamuelSVD/SpellCheck.svg)](../../releases/latest)

# SpellCheck
A .Net console application that allows the user to spell-check text files.
It provides the option to modify the dictionary used in spell-check and add case-sensitive words, for example acronyms.

# Usage
	SpellCheck.exe <path-to-compare> [path-to-dictionary]
		<path-to-compare>     path to file to spell check
		[path-to-dictionary]  path to file to use as dictionary
		--make                optional parameter to create dictionary file

## Special cases
Worth noting, there are special cases where spell checking is not executed.

Spell checking is bypassed if:
- the word starts with a backslash
- the word is encased in ```<``` and ```>```, for example ```<div>``` in html files
- the word is not alphanumeric
- the word is strictly numeric

# References
Dictionary copied from https://github.com/dwyl/english-words

