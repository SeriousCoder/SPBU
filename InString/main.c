/*
    Author: Tarasenko Nikita
    Problem: "Search substring"
 
 */

#include <stdio.h>
#include <stdlib.h>



int main() 
{
    char *s1, *s2;
    int num, i = 0;
    
    s1 = (char*)malloc(sizeof(char) * 1000);
    s2 = (char*)malloc(sizeof(char) * 1000);
    
    if (!s1 || !s2)
    {
        printf("Not enough memory!");
        exit (NOT_ENOUGH_MEMORY);
    }
    
    printf("Enter S1: ");
    scanf("%s", s1);
    
    printf("Enter S2: ");
    scanf("%s", s2);
    
    while (s1[i])
    {
        if (s1[i] == s2[0])
        {
            int j = 1, bool = 1;
            while(s2[j] && s1[i + j] && bool)
            {
                if (s2[j] != s1[i + j])
                {
                    bool = 0;
                }
                else
                {
                   j++; 
                }
            }
            if (bool && !s2[j])
            {
                num++;
            }
            if (!s1[i + j])
            {
                break;
            }
            i += j - 1;
        }
        else
        {
            i++;
        }
       
    }

    printf("Number of entries: %d", num);
    
    free(s1);
    free(s2);
    
    return 0;
}

