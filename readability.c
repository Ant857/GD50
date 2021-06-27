#include <ctype.h>
#include <cs50.h>
#include <stdio.h>
#include <string.h>
#include <math.h>

int compute_letters(string inputText);
int compute_words(string inputText);
int compute_sentences(string inputText);

int main(void)
{
    string text = get_string("Text: ");
    float letters = compute_letters(text);
    float words = compute_words(text);
    float sentences = compute_sentences(text);
    float L = letters / words * 100;
    float S = sentences / words * 100;
    float index = 0.0588 * L - 0.296 * S - 15.8;
    int roundedIndex = round(index);
    
    if(roundedIndex < 1)
    {
        printf("Before Grade 1\n");
    }
    else if(roundedIndex > 15)
    {
        printf("Grade 16+\n");
    }
    else
    {
        printf("Grade %i\n", roundedIndex);
    }
}

int compute_letters(string inputText)
{
    int totalLetters = 0;
    for(int i = 0, n = strlen(inputText); i < n; i++)
    {
        if(isupper(inputText[i]) || islower(inputText[i]))
        {
            totalLetters++;
        }
    }
    return totalLetters;
}

int compute_words(string inputText)
{
    int totalWords = 0;
    for(int i = 0, n = strlen(inputText); i < n; i++)
    {
        if(inputText[i] == ' ')
        {
            totalWords++;
        }
    }
    return totalWords + 1;
}

int compute_sentences(string inputText)
{
    int totalSentences = 0;
    for(int i = 0, n = strlen(inputText); i < n; i++)
    {
        if(inputText[i] == '.' || inputText[i] == '!' || inputText[i] == '?')
        {
            totalSentences++;
        }
    }
    return totalSentences;
}