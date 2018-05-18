### What is this repository for? ###

hsp.Reaper is a C# library for reading and parsing Reaper RPP Project Files.

Usage
-----

Use the static class ReaperProjectFile to load the file. It returns an instance of `ReaperProject` which is the root of every project.

    ReaperProject rpp = ReaperProjectFile.Load("d:\path\to\project\file.rpp");
    
Using `ReaperProject.GetElements()` returns every child element inside the project.

For instance: if you want to retrieve all tracks that are in a project you would then write:

    IEnumerable<ReaperTrack> tracks = rpp.GetElements().OfType<ReaperTrack>();
    foreach (var track in tracks)
      Console.WriteLine(String.Format("{0} {1}", track.ID, track.Name));
	  
Feature
-------

Currently the following classes are (partially) implemented:
- `ReaperProject`: every project's root element
- `ReaperTrack`: represents a track
- `ReaperProjectNotes`: represents the project's notes
- `ReaperMediaItem`: represents a media item
- `ReaperMediaItemSource`: represents the source of a media item
- `ReaperFX`: represents a FX instance
- `ReaperFXPlugin`: represents a plugin with it's data (currently only VST is supported)
- `ReaperFXChain`: represents the FX chain of a track