#include <cs50.h>
#include <stdio.h>
#include <math.h>

int main(void)
{
    long n = get_long("Number: ");

    long x = n;
    int sum1 = 0;
    int sum2 = 0;
    int i = 0;
    int count = 0;
    
    do
    {
        x = x / 10;
        i++;
    }
    while (x > 0);
    
    if (i != 13 && i != 15 && i != 16)
    {
        printf("INVALID\n");
    }
    else
    {
        x = n;

        do
        {
            int digit = (int)(x % 10);
            if (count % 2 == 1)
            {
                if(digit * 2 > 9)
                {
                    int combinedDigit = digit * 2;
                    sum1 = sum1 + (combinedDigit % 10);
                    combinedDigit = combinedDigit / 10;
                    sum1 = sum1 + (combinedDigit % 10);
                }
                else
                {
                    sum1 = sum1 + (digit * 2);
                }
            }
            x = x / 10;
            count++;
        }
        while (x > 0);
    
        count = 0;
        x = n;
    
    
        do
        {
            count++;
            int digit = (int)(x % 10);
            if (count % 2 == 1)
            {
                sum2 = sum2 + (digit);
            }
            x = x / 10;
        }
        while (x > 0);
    
        x = n;
    
        if((sum1 + sum2) % 10 == 0)
        {
            int firstNumber = (int)((double)x / (pow(10, i - 1)));
            int firstTwoNumbers = (int)((double)x / (pow(10, i - 2)));
            
            if(firstNumber == 4)
            {
                if(i == 13 || i == 16)
                {
                    printf("VISA\n");
                }
            }
            else if(firstTwoNumbers == 34 || firstTwoNumbers == 37)
            {
               if(i == 15)
                {
                    printf("AMEX\n");
                }
            }
            else if(firstTwoNumbers > 50 && firstTwoNumbers < 56)
            {
               if(i == 16)
                {
                    printf("MASTERCARD\n");
                }
            }
            else
            {
                printf("INVALID\n");
            }
        }
        else
        {
            printf("INVALID\n");
        }
    }
}
