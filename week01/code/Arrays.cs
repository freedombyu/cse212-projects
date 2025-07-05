public static class Arrays 
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN FOR SOLVING MultiplesOf FUNCTION:
        // Step 1: Create a new double array with the specified length
        // Step 2: Use a loop to iterate through each position in the array (from 0 to length-1)
        // Step 3: For each position i, calculate the multiple by multiplying the number by (i+1)
        //         - Position 0 gets number * 1 = the original number
        //         - Position 1 gets number * 2 = first multiple
        //         - Position 2 gets number * 3 = second multiple, etc.
        // Step 4: Store each calculated multiple in the corresponding array position
        // Step 5: Return the completed array

        // IMPLEMENTATION:
        // Step 1: Create array with specified length
        double[] multiples = new double[length];
        
        // Step 2-4: Loop through each position and calculate the multiple
        for (int i = 0; i < length; i++)
        {
            // Step 3: Calculate multiple - position i gets number * (i+1)
            multiples[i] = number * (i + 1);
        }
        
        // Step 5: Return the completed array
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN FOR SOLVING RotateListRight FUNCTION:
        // Step 1: Calculate the split index: data.Count - amount.
        //         - This is where the list will be split to rotate.
        // Step 2: Create a temporary list to hold the last 'amount' elements (the part to move to front).
        // Step 3: Create another temporary list to hold the first (Count - amount) elements.
        // Step 4: Clear the original list.
        // Step 5: Add the elements from the "last" list to the original list.
        // Step 6: Add the elements from the "first" list to the original list.
        // Step 7: Now, the original list is rotated to the right by 'amount'.
        
        // Understanding the rotation:
        // For example: {1,2,3,4,5,6,7,8,9} rotated right by 3 becomes {7,8,9,1,2,3,4,5,6}
        // The last 3 elements (7,8,9) move to the front
        // The first 6 elements (1,2,3,4,5,6) move to the back

        // IMPLEMENTATION:
        int count = data.Count;
        
        // Handle edge cases - no rotation needed if amount is 0 or if amount equals count
        if (amount <= 0 || amount % count == 0)
        {
            return;
        }
        
        // Use modulo to handle cases where amount might be larger than count
        amount = amount % count;
        
        // Step 1: Calculate split point and Step 2-3: Use GetRange to efficiently extract sublists
        List<int> last = data.GetRange(count - amount, amount);    // Last 'amount' elements
        List<int> first = data.GetRange(0, count - amount);        // First 'count - amount' elements
        
        // Step 4: Clear the original list
        data.Clear();
        
        // Step 5-6: Add elements in new order - last elements first, then first elements
        data.AddRange(last);
        data.AddRange(first);
    }
}