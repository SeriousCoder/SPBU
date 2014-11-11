#include <stdio.h>

int Sign(int n);

int main(void)
{
    int n;
	
    printf("Enter integer: ");
    scanf("%d", &n);
	
    printf ("%d\n", Sign(n));
	
    return 0;
}

int Sign(int n)
{
    return ((n >> 0x1F) + !( n >> 0x1F) + ~( !n + ~0 ));
}