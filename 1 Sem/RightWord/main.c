/*
    Author: Tarasenko Nikita
    Problem: "RightWord"
 
 */

#include <stdio.h>

int RightWord(int x ,int n);

int main(void) 
{
	int x, n;
	
	printf("Enter two integers(x and n): ");
	scanf("%d %d", &x, &n);
	
	printf("%d", RightWord(x, n));
	
	return 0;
}

int RightWord(int x ,int n)
{
	return !((x >> (n - 1)) + (!!(x >> (n - 1))));
}