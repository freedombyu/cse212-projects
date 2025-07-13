using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities and dequeue them
    // Expected Result: Items should be dequeued in priority order (highest first)
    // Defect(s) Found: 
    // - Dequeue may not properly find the highest priority item
    // - Items may not be removed correctly from the queue
    public void TestPriorityQueue_BasicPriorityOrdering()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 5);
        priorityQueue.Enqueue("Medium", 3);
        
        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items with same priority and dequeue them
    // Expected Result: Items with same priority should be dequeued in FIFO order
    // Defect(s) Found: 
    // - When multiple items have same priority, may not return the first one enqueued
    // - FIFO ordering within same priority level may be broken
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("First", 3);
        priorityQueue.Enqueue("Second", 3);
        priorityQueue.Enqueue("Third", 3);
        
        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Mix of same and different priorities
    // Expected Result: Higher priority items come first, same priority items follow FIFO
    // Defect(s) Found: 
    // - Mixed priority and FIFO ordering may not work correctly
    // - Priority comparison logic may be flawed
    public void TestPriorityQueue_MixedPriorities()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Low1", 1);
        priorityQueue.Enqueue("High1", 5);
        priorityQueue.Enqueue("Low2", 1);
        priorityQueue.Enqueue("High2", 5);
        priorityQueue.Enqueue("Medium", 3);
        
        Assert.AreEqual("High1", priorityQueue.Dequeue());
        Assert.AreEqual("High2", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low1", priorityQueue.Dequeue());
        Assert.AreEqual("Low2", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue
    // Expected Result: Exception should be thrown with appropriate error message
    // Defect(s) Found: 
    // - May not throw exception when queue is empty
    // - Exception message may be incorrect
    // - May throw wrong type of exception
    public void TestPriorityQueue_EmptyQueueException()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue and dequeue single item
    // Expected Result: Single item should be returned correctly
    // Defect(s) Found: 
    // - Single item handling may be broken
    // - Queue state may not be updated correctly after single operation
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("OnlyItem", 10);
        Assert.AreEqual("OnlyItem", priorityQueue.Dequeue());
        
        // Should be empty now
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown for empty queue.");
        }
        catch (InvalidOperationException)
        {
            // Expected
        }
    }

    [TestMethod]
    // Scenario: Enqueue items with negative priorities
    // Expected Result: Negative priorities should work, higher (less negative) values have higher priority
    // Defect(s) Found: 
    // - Negative priority handling may be incorrect
    // - Priority comparison with negative numbers may be flawed
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("VeryLow", -10);
        priorityQueue.Enqueue("Low", -5);
        priorityQueue.Enqueue("High", 5);
        priorityQueue.Enqueue("Medium", 0);
        
        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
        Assert.AreEqual("VeryLow", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue after dequeue operations
    // Expected Result: New items should be properly positioned based on priority
    // Defect(s) Found: 
    // - Adding items after dequeue operations may not work correctly
    // - Queue state may be corrupted after mixed operations
    public void TestPriorityQueue_EnqueueAfterDequeue()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("First", 3);
        priorityQueue.Enqueue("Second", 1);
        
        Assert.AreEqual("First", priorityQueue.Dequeue());
        
        priorityQueue.Enqueue("Third", 5);
        priorityQueue.Enqueue("Fourth", 2);
        
        Assert.AreEqual("Third", priorityQueue.Dequeue());
        Assert.AreEqual("Fourth", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
    }
}