A simple application to test out different multi-threading possibilities.

To Use The Application
----------------------

1. Go to the "ConsolTest" class in the "Tests" project

2. Pick your chosen implementation of "ITaskHandler" and pass it into the "ImageRetriever"
   - why not create your own implementation that will process tasks in a custom way
   
3. Change the URL passed into downloader.Download()

4. Run the test "DownloadImages()" where all the above code lives

5. Observe the console window to see which images were fetched and which could not be

6. Stick a break point if you want to get the data for each image to verify it works