#include <ctype.h>
#include <cs50.h>
#include <stdio.h>
#include <string.h>
#include <math.h>

const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

int main(int argc, string argv[])
{
    if(argc != 2)
    {
        printf("Please provide only one command line argument.");
        return 1;
    }

    if(strlen(argv[1]) != 26)
    {
        printf("Key must be 26 letters long.");
        return 1;
    }

    int letters[26];

    for(int i = 0, n = strlen(argv[1]); i < n; i++)
    {
        if(!((argv[1][i] >= 'A' && argv[1][i] <= 'Z') || (argv[1][i] >= 'a' && argv[1][i] <= 'z')))
        {
            printf("Key must only contain letters.");
            return 1;
        }
        else if (argv[1][i] >= 'a' && argv[1][i] <= 'z')
        {
            argv[1][i] = toupper(argv[1][i]);
        }

        for (int j = 0; j < 26; j++)
        {
            if (argv[1][i] == letters[j])
            {
                printf("Key must not repeat letters.");
                return 1;
            }
        }

        letters[i] = argv[1][i];
    }

    string plaintext = get_string("plaintext: ");
    string ciphertext = plaintext;

    for(int i = 0, n = strlen(plaintext); i < n; i++)
    {
        bool isUppercase;
        
        if(isupper(plaintext[i]))
        {
            isUppercase = true;
        }
        else if(islower(plaintext[i]))
        {
            isUppercase = false;
        }

        plaintext[i] = toupper(plaintext[i]);

        for(int j = 0; j < 26; j++)
        {
            if(alphabet[j] == plaintext[i])
            {
                ciphertext[i] = argv[1][j];
                break;
            }
        }

        if (!isUppercase)
        {
            ciphertext[i] = tolower(ciphertext[i]);
        }
    }
    printf("ciphertext: %s\n", ciphertext);
    return 0;
}