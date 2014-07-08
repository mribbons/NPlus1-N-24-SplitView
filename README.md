NPlus1-N-24-SplitView
=====================

See Stuart Lodge's N=24 SplitView Tutorial
(http://slodge.blogspot.co.uk/2013/05/n24-splitviewpresenter-n1-days-of.html)

On iPad 7.1 the split view doesn't work.

It looks like UISplitViewController needs ViewController[] to be populated at runtime.

My possibly bad solution is to recreate the SplitViewController each time a view request is received.

Resolve issue where UISplitView doesn't update views at runtime
