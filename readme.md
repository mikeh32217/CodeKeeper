The CodeKeeper project provides simple tag replacement functionality, written specifically for managing code snippets where a Snippet manager is not available.

The application is currently in Beta and there is still a lot I would like to do with it but I'm going to leave it here for now and see if there's any interest before moving on. If not I willn develop it for my own use.

The Main window, shown below is the where a folder with the files that are to be worked on are displayed and selected.

![CodeKeeper_V_1 0](https://user-images.githubusercontent.com/3386194/117689618-f25fd480-b187-11eb-9df4-a0e79aae7d11.png)

There are two editors available; the Tag editor where key value pairs are created that when processed the key will be replaced by the value. At this time there are several keywords; filename, grabs the filename of the current file being worked on, Prompt, will display a popup where the text to insert at the point of the prompt is entered and date which replaces the current dat with the format sepecifed in the tag. There is also a Snippet editor where snippets of text and tags are entered and when processed will be inserted in the file at the point specified.

Below is an image of the window where the raw data is displayed, iut can be edited and the final output previewed and saved in the Preview window.  

![Preview_Output_Window](https://user-images.githubusercontent.com/3386194/117689635-f5f35b80-b187-11eb-943e-bbd4a6fa2f24.png)

At the moment this is a very simple application, it was an appliction that I used to lean WPF and not only did I learn a lot but I got a faily decent applciaation out of it.
