Some silly data structure stuff.

The first is red/black trees - implemented pretty much straight out of the wikipedia page for the structure.  I read somewhere that the C# Dictionary type uses a red/black tree implementation.  So comparing this one to Dictionary yields 31359ms for mine, vs 632ms for Dictionary (for 10 million unique keys).  So this is very much a let's see how it works comparison.  I haven't compared over look ups, though.

Added bloom filter, based on this page: https://www.geeksforgeeks.org/bloom-filters-introduction-and-python-implementation/ 
Have used the murmur3 hash via a nuget package I found - should have abstracted this away a bit.  Very much 32 bit oriented, and hardcoded in.
At the moment, if you need x hashes, it produces x hash clients with seeds 0 to x-1 which probably isn't great either.

To do: skip lists (need to work out the choice of levels).
To do: some concurrent or non-blocking structures like queues.
