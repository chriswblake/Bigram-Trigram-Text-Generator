#define MAX_WORD_LEN    40
#define MAX_WORDS       1000

#define FIRST_WORD		0
#define MIDDLE_WORD     1
#define LAST_WORD       2

#define START_SYMBOL    0
#define END_SYMBOL		1

static int curWord = 2;
char wordVector[MAX_WORDS][MAX_WORD_LEN];
int bigramArray[MAX_WORDS][MAX_WORDS];
int sumVector[MAX_WORDS];

int main(int argc, char *argv[])
{
	char filename[40];
	int  debug = 0;

	/* Parse the options from the command line */
	parseOptions(argv, argc, &debug, filename);

	/* Seed the random number generator */
	srand(time(NULL));

	bzero(bigramArray, sizeof(bigramArray));

	strcpy(wordVector[0], "<START>");
	strcpy(wordVector[1], "<END>");

	/* Parse the Corpus */
	parseFile(filename);

	if (debug) emitMatrix();

	/* Randomly generate a sentence */
	buildSentence();

	return 0;
}

//Command line options
void parseOptions(char *argv[], int argc, int *dbg, char *fname)
{
	int opt, error = 1;

	*dbg = 0;

	if (argc > 1)
	{
		while ((opt = getopt(argc, argv, "vf:")) != -1)
		{
			switch (opt)
			{
				case 'v':
					//Verbose Mode
					*dbg = 1;
					break;

				case 'f':
					//Filename option
					strcpy(fname, optarg);
					error = 0;
					break;

				default:
					//Any other option is an error
					error = 1;
			}

		}

	}

	if (error)
	{
		printf("\nUsage is : \n\n");
		printf("\t%s -f <filename> -v\n\n", argv[0]);
		printf("\t\t -f corpus filename\n\t\t -v verbose mode\n\n");
		exit(0);
	}

	return;
}

		//File to BIGRAM array
		void parseFile(char *filename)
{
	FILE *fp;
	int  inp, index = 0;
	char word[MAX_WORD_LEN + 1];
	int  first = 1;

	fp = fopen(filename, "r");

	//Cycle through entire file
	while (!feof(fp))
	{
		//Read a character, by code
		inp = fgetc(fp);

		//Check for end of file
		if (inp == EOF)
		{
			if (index > 0)
			{
				//For the last word, update the matrix accordingly
				word[index++] = 0;
				loadWord(word, LAST_WORD);
				index = 0;
			}
		}
		else if (((char)inp == 0x0d) || ((char)inp == 0x0a) || ((char)inp == ' ')) //spaces
		{
			if (index > 0)
			{
				//shift last value in array
				word[index++] = 0;
				if (first)
				{
					//First word in a sequence
					loadWord(word, FIRST_WORD);
					index = 0;
					first = 0;
				}
				else
				{
					//Middle word of a sequence
					loadWord(word, MIDDLE_WORD);
					index = 0;
				}
			}
		}
		else if (((char)inp == '.') || ((char)inp == '?'))
		{
			//Handle punctuation by ending the current sequence
			word[index++] = 0;
			loadWord(word, MIDDLE_WORD);
			loadWord(word, LAST_WORD);
			index = 0;
			first = 1;
		}
		else
		{
			//Skip white space
			if (((char)inp != 0x0a) && ((char)inp != ','))
			{
				word[index++] = (char)inp;
			}
		}
	}

	fclose(fp);
}

		//Word to BIGRAM array
		void loadWord(char *word, int order)
{
	int wordIndex;
	static int lastIndex = START_SYMBOL;

	//First, see if the word has already been recorded
	for (wordIndex = 2; wordIndex < curWord; wordIndex++)
	{
		if (!strcmp(wordVector[wordIndex], word))
			break;
	}

	//add to end of array
	if (wordIndex == curWord)
	{
		if (curWord == MAX_WORDS)
		{
			printf("\nToo may words, increase MAX_WORDS\n\n");
			exit(-1);
		}

		//Doesn't exist, add it in
		strcpy(wordVector[curWord++], word);
	}

	//At this point, we have a wordIndex that points to the current word vector.

	//Store the word in the correct area
	if (order == FIRST_WORD)
	{
		//Load word as the start of a sequence
		bigramArray[START_SYMBOL][wordIndex]++;
		sumVector[START_SYMBOL]++;
	}
	else if (order == LAST_WORD)
	{
		//Load word as the end of a sequence
		bigramArray[wordIndex][END_SYMBOL]++;
		bigramArray[END_SYMBOL][wordIndex]++;
		sumVector[END_SYMBOL]++;
	}
	else
	{
		//Load word as the middle of a sequence
		bigramArray[lastIndex][wordIndex]++;
		sumVector[lastIndex]++;
	}

	lastIndex = wordIndex;

	return;
}


int buildSentence(void)
{
	int word = START_SYMBOL;
	int max = 0;

	printf("\n");

	/* Start with a random word */
	word = nextWord(word);

	/* Loop until we've reached the end of the random sequence */
	while (word != END_SYMBOL) {

		/* Emit the current word */
		printf("%s ", wordVector[word]);

		/* Choose the next word */
		word = nextWord(word);

		/* Only allow a maximum number of words */
		max += getRand(12) + 1;

		/* If we've reached the end, break */
		if (max >= 100) break;

	}

	/* Emit a backspace, '.' and a blank line */
	printf("%c.\n\n", 8);

	return 0;
}
	int nextWord(int word)
{
	int nextWord = (word + 1);
	int max = sumVector[word];
	int lim, sum = 0;

	//Define a limit for the roulette selection
	lim = getRand(max) + 1;

	while (nextWord != word)
	{
		//Bound the limit of the array using modulus
		nextWord = nextWord % curWord;

		//Keep a sum of the occurrences (for the roulette wheel)
		sum += bigramArray[word][nextWord];

		//If we've reached our limit, return the current word
		if (sum >= lim) 
			return nextWord;	

		//For the current word (row), go to the next word column
		nextWord++;
	}

	return nextWord;
}

void emitMatrix(void)
{
	int x, y;

	printf("\n");
	for (x = 0; x < curWord; x++) {
		printf("%20s : ", wordVector[x]);
		for (y = 0; y < curWord; y++) {
			printf("%d ", bigramArray[x][y]);
		}
		printf(" : %d\n", sumVector[x]);
	}
}
