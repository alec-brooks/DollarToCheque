===============================================================================
DollarToCheque
===============================================================================
An application for converting numerical input into words,and formatting it like
a cheque.

Usage: DollarToCheque.exe dollarAmount
A valid dollarAmount is a number between 0 and 999999999999999999, and must
either be a whole number, or a number with two decimal places.

===============================================================================
Approach
===============================================================================
This program converts a dollar figure into a cheque format by first reading in 
an input and validating it using a regular expression. The string is then 
separated into a dollar string and a cent string. The dollar string is split 
into groups of three, if the string isn't divisible by three then the odd group
is put at the front. This was done as, in English, numbers are often grouped 
into lots of hundreds.

12345 would become "12" + "345"

After this, the individual groups were converted into text. This was achieved 
by turning the first number into text plus a string saying "HUNDRED AND", then 
turning the next number into an English word for numbers in the tens column, 
such as twenty or thirty. This was followed by a dash and then the last number 
being converted into a textual representation of itself.

"345" would become "THREE HUNDRED AND FORTY-FIVE"

This was achieved using three methods, one for each size string. In the
methods dealing with more than strings of length one, quotients and modulus
functions were used to find the appropriate word from the list of words.

To make FORTY-FIVE, the fourth element of the tens list is needed to be 
combined with the fifth element of the ones list.

45/10 = 4		45%10 = 5

After the number has been converted to text, a word denoting scale is applied,
something like "THOUSAND" or "MILLION". The correct word was chosen by 
comparing the length of the list of "scale" words and the list of dollar blocks
plus one. It requires the plus one as the last block doesn't require a scale
indicator.

After repeating this process for each block, it then adds the cent value, and 
appropriate text.

===============================================================================
Reflection On Approach
===============================================================================
When writing cheques, or speaking any number out loud, generally the number is
separated into groups of three, with a hundreds, tens and ones column. This 
led me to have the core of my program revolve around splitting the string into
threes and dealing with the strings of three one at a time.

I could have also run through the string iteratively and added relevant words
when they were needed, but since the program would have to keep track of where
the hundreds would have to go, it would need to keep running modulo 3 functions
so I thought that keeping the groups of three seperate would have been easier.

I also decided to keep the dollar value as a string right up until the moment
of conversion to text. I toyed with the idea of refactoring the program so that
the conversion happens right after the string is split, because I ended up 
having a lot of calls to conversion functions, which could have possibly been
minimised if they had been turned into ints from the start. However, time 
restraints stopped me from doing so.

===============================================================================
Test Plan
===============================================================================
Input validation		-	Whole Number evaluates true
						-	Number with two decimal points evaluates true
						-	Number with one decimal point evaluates false
						-	Number with more than two decimal points is false
						-	Non Number evaluates false
						-	String with numbers and digits evaluates false
						-	Negative Number evaluates false
						-	Zero evaluates false
						-	Quintillion evaluates false
ConvertDollarToText		-	Test less that one hundred amount
						-	Test amount in the thousands
						-	Test amount in the millions
OneToText				-	Number returns correct number
						- 	Zero returns blank
TwoToText				-	Zero returns blank
						-	Single returns text single
						-	Teen returns text teen
						-	Multiple of ten returns text multiple of ten
						-	Mixed Number returns mixed number
ThreeToText				-	Zero returns blank
						-	Single returns text single
						-	Double returns text double
						-	Hundred returns text Hundred
SplitStringIntoBlocks	-	Str of len 1 returns list of same string
						-	Str of len 2 returns list of same string
						-	Str of len 3 returns list of same string
						-	Str if len 6 returns two strings
