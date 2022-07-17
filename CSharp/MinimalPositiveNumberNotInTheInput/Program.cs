class Solution {
    public int solution(int[] A) {
        var minimumValue = A.Min();
        if(minimumValue > 1)
            return 1;

        var hashSet = new HashSet<int>(A);
        for(var i = 1; i <= A.Length; i++)
        {
           if(!hashSet.Contains(i)) 
               return i;
        }
        return A.Max() + 1;
    }
}
