#include <stdio.h>
#include <stdlib.h>



int main() 
{
    char *s1, *s2;
    int num, i = 0;
    
    s1 = (char*)malloc(sizeof(char) * 1000);
    s2 = (char*)malloc(sizeof(char) * 1000);
    
    printf("Enter S1: ");
    scanf("%s", s1);
    
    printf("Enter S2: ");
    scanf("%s", s2);
    
    while (s1[i] != '\0')
    {
        if (s1[i] == s2[0])
        {
            int j = 1, bool = 1;
            while(s2[j] != '\0' && s1[i + j] != '\0' && bool)
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
            if (bool == 1 && s2[j] == '\0')
            {
                num++;
            }
            if (s1[i + j] == '\0')
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
    
    return 0;
}

