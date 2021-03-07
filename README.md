# ProjectGenerator
 Visual studio projects don't support wild cards natively (or if they do you lose access to some in visual studio features). 
 I made this so I can more dynamically update some of my C++ project files.
 
# How it works.
 The exe takes in a template xml file and makes specific modifications to create a project file with the edits I want.
 
 The exe takes up to two arguments.
 Argument 1: The template xml file for generating the project file
 Argument 2(optional): The output file path (by default it uses Argument1 but with the vcxproj extension)
 
 Included in the project is an example "ProjectTemplate.xml" basically it is just a common project file, except it has some custom elements added to it.
 When we process the template xml document we find instances of our custom elements, process the attributes to do something logic e.g find all files matching a directory and search pattern
