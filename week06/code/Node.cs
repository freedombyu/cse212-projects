public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // Problem 1 Solution: Check for duplicates to ensure unique values only
        if (value == Data)
            return; // Don't insert duplicate values

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // Problem 2 Solution: Search using BST properties
        if (value == Data)
            return true; // Found the value

        if (value < Data)
        {
            // Search in left subtree
            return Left != null && Left.Contains(value);
        }
        else
        {
            // Search in right subtree
            return Right != null && Right.Contains(value);
        }
    }

    public int GetHeight()
    {
        // Problem 4 Solution: Calculate height recursively
        int leftHeight = (Left != null) ? Left.GetHeight() : 0;
        int rightHeight = (Right != null) ? Right.GetHeight() : 0;
        
        // Height is 1 (current node) plus the maximum height of subtrees
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}