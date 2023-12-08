namespace Day8;

public class LCMFinder
{
    public static long Gcd(long a, long b)
    {
        if (b == 0)
            return a;
        return Gcd(b, a % b);
    }


    public static long FindLcm(long[] arr, long n)
    {
        long ans = arr[0];

        for (long i = 1; i < n; i++)
            ans = (((arr[i] * ans)) /
                   (Gcd(arr[i], ans)));

        return ans;
    }
}