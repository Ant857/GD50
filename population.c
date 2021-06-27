#include <cs50.h>
#include <stdio.h>

int main(void)
{
    int startSize;
    do
    {
        startSize = get_int("Starting Population Size: ");
    }
    while (startSize < 9);
    
    int endSize;
    do
    {
        endSize = get_int("Ending Population Size: ");
    }
    while (endSize < startSize - 1);
    
    int population = startSize;
    int years = 0;
    
    do
    {
        years++;
        int newLlamas = population / 3;
        int deadLlamas = population / 4;
        population = population + newLlamas - deadLlamas;
    }
    while (population < endSize);
    
    printf("Years: %i\n", years);
}