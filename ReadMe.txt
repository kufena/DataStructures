Some silly data structure stuff.

The first is red/black trees - implemented pretty much straight out of the wikipedia page for the structure.  I read somewhere that the C# Dictionary type uses a red/black tree implementation.  So comparing this one to Dictionary yields 31359ms for mine, vs 632ms for Dictionary (for 10 million unique keys).  So this is very much a let's see how it works comparison.  I haven't compared over look ups, though.

To do: skip lists (need to work out the choice of levels) and bloom filters (need seven fast hashes.)
To do: some concurrent or non-blocking structures like queues.
