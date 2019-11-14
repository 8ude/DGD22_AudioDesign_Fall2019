# DGD22_AudioDesign_Fall2019
Fall 2019 Section of Audio Design for Games at LIU Post

See the [Wiki](https://github.com/8ude/DGD22_AudioDesign_Fall2019/wiki) for Helpful Links and slides from past weeks

You can find the [Syllabus here](https://github.com/8ude/DGD22_AudioDesign_Fall2019/blob/master/2019_Fall_CoreyBertelsen_AudioDesignForGamesDGD22%20001.pdf)

FINAL PROJECT
------

Your final project for this class is going to be to make an **audio only game** - as in, a game that has no visual feedback.

If you need some inspiration, [here is a website dedicted to audio-only games](https://audiogames.net/list-games/)

You can work in groups (no more than 3 please), and you can use Unity and whatever tools you've used in the class.

On **Thursday Nov 21** I will expect you to have your group decided, and your basic concept figured out.

You will present your games at the last class on **Thursday December 5** 

Put your final build together with a text or pdf file that contains:
- your game name
- your team members names
- instructions on how to play

And upload a compressed file [HERE](https://drive.google.com/open?id=1n4s3YCHstiJ3mV2EGqxQq1QWx091Wsau)


WEEK 10 ASSIGNMENT: MUSIC
------
UPDATE - [here's a video tutorial on how to make some basic music and get it into FMOD](https://drive.google.com/file/d/18PHD8v154JqQGO5j9gvaWHmH-fm0Dbxj/view?usp=sharing)

Make 2 sections of music - think of them as an "A section" and a "B section."  Here's a [Sample Reaper Project with some instruments set up](https://drive.google.com/file/d/1T7Pv7u1g1VZosPoHqLuHV9vSawDRu1ma/view?usp=sharing). If you don't like working in Reaper, I recommend trying something like [Chrome's Song Maker tool](https://musiclab.chromeexperiments.com/Song-Maker/)

If you're working on your own computer, you can make use of some of the apps and plugins on the [Musical Resources Page](https://github.com/8ude/DGD22_AudioDesign_Fall2019/wiki/Musical-Resources)

Bring the Stems (Melody, Bass, Percussion, etc) into FMOD and add some interactivity that you connect to a parameter.  

Some ideas (just suggestions, use what works for your music):
- have instruments fade in/out based on a "PlayerStatus" parameter
- change song sections based on a "PlayerLocation" parameter
- make some ambient fragments, then use the ScattererInstrument to trigger them at random

When you're done, zip your FMOD Project and [UPLOAD IT HERE](https://drive.google.com/drive/folders/1kC0ewoHoJ6sX8asBrU5fLippFZvlR_P_?usp=sharing)



WEEK 9 ASSIGNMENT: SOUND SPACE
------
UPDATE - [here's a refresher tutorial on what we covered in Thursday's class.](https://drive.google.com/file/d/1XJ_36aDL4MXitZgNpxlnKwxGT-rFeAkO/view?usp=sharing) The ending doesn't have sound in the Unity scene (issues with my screencap software), but rest assured I could here the creepy baby sound.

Happy Halloween!  In class we started making a haunted house using Unity and Google's Resonance Audio (Available [HERE](https://github.com/resonance-audio/resonance-audio-unity-sdk/releases))  You can either continue with that, or create a different sonic space.

Start by taking the Resonance Audio Demo Scene, copying it like we did in class, then remove everything but the Player and the point light.  Then greybox your environment using unity primitives (planes, cubes etc), and place your audio sources in the scene.  Make sure these have the ResonanceAudioSource component on them.

You must use:
 - At least 7 unique sounds
 - At least 3 unique spaces (I recommend using the ResonanceAudioRoom prefab - available under Assets > ResonanceAudio > Prefabs )


Also, if you want to do the creepy backwards reverb sound but forgot how, here's a [tutorial](https://drive.google.com/file/d/1d18AdyQfx8fboDTTr75TYRILAKgjfs3l/view?usp=sharing)

If you want a sound to play when the player enters an area, add a 3D collider to the game object with the sound, set "IsTrigger" to true, and add [this script](https://drive.google.com/file/d/1iXDeRDorEmIpS4t94d8U3BsBCTl_5z-5/view?usp=sharing)

Make a build (Windows Preferred) **and upload it [HERE](https://drive.google.com/drive/folders/1dcUaA5YRG-ZneWmEGHZXkmHOAA0vPzDT?usp=sharing)**

CRITICAL LISTENING ASSIGNMENT UPDATE
------
This assignment is now a paper, due **November 28 (Thanksgiving)**

Critical Listening - play a game from the provided list, [available here](https://docs.google.com/spreadsheets/d/1Bn5J00GZ341uofRyMuntdbZQDFrSYf6LfDXIevePOCA/edit?usp=sharing), or make a case for a different game.  DO NOT watch a let's play.  Your play session should be about 2-3 hours

Answer the following in 2-3 pages:

* How would you describe the overall audio aesthetic, mood, or feeling of the game?  How do each of the audio layers (music, ambience, and sound effects) support that aesthetic
* What do you think are the most important sounds, and why? How do sounds change in response to player action (some sounds may be randomized, or connected to in-game physics in interesting ways)?
* Look up some facts, videos, or interviews about the creation of the game audio - what’s interesting about the techniques or technology used?

WEEK 7/8 ASSIGNMENT- 3D Reskin
------

**LINK TO UPDLOAD BUILDS [HERE](https://drive.google.com/drive/folders/19DXhODLOndGAaPeLQT_hO5f1IJruc7ea?usp=sharing)**

The game Otto the Combo Butler is filled with placeholder sounds, and needs new audio!  Clone the repo, open the Otto Project in both Unity and FMOD, and replace the audio.

I've included a few [video tutorials in a folder here to get you started](https://drive.google.com/open?id=1a6YK7YoSBG6bv_-HzR3K7W7XYiS7eU-9).

[This video is a walkthrough of the game.](https://drive.google.com/open?id=189lJJme1fQj8XuAUbtkyRW_BFE7bcCS4)

There will be a **progress check on October 24** - aim for about half of the sounds in the game.  Builds will be due on **Oct 31**

You should only really need Unity to test the game and make builds.  Other than that, all of the assignment is going to be done in FMOD Studio

You must use **at least one sound that you've recorded yourself.**  Additionally, you can use:
* Whatever you find on the internet, particularly from Freesound.org
* Any effects.


I'm using the [same rubric as last time,](https://docs.google.com/document/d/1WEAa_ZDIDO9yXnfGNs9Qb0WIcTCkBZF3NWzVn5RDqNk/edit?usp=sharing).  Once again, a few things to note:

You will lose points for:
* Any Unmodified samples.  You need to modify and/or layer the sounds from freesound in some way (changing pitch, stretching, adding effects, etc).
* Repeating sounds for different actions  Again, treat your samples like ingredients.
* Sounds that start "late" - your sounds should have little to no silence (0.01 seconds max) at the beginning
* Sounds that end "early" - your sounds should always fade to silence.
* Loops with clearly audible seams (I can tell when the loop happens)

Aim for 
* clarity of actions (can I tell what's going on based on sound?) 
* consistency (does the sound seem to match with the visuals and animations?)
and, most importantly, 
* a sense of style/aesthetics (are the sounds augmenting the game in a way that gives it life and character?).

WEEK 6 ASSIGNMENT- Recordings
------

Make **3 sound effects** from recordings.  Do some noise removal on these.

Then, name them with the material, the type of motion used, and whether there is one sound or multiple.  For example: WoodCrashMultiple.wav

Make a folder with your name here and upload the [SFX HERE](https://drive.google.com/drive/folders/1YljApbJ_tgyLti7Ek0FtEptwuPxEKBZO?usp=sharing)

In addition, **record 3 ambiences** from interesting spaces (They **cannot** be spaces at the game center, and only one of them can be the subway!).  Your phone will work fine for this, but keep your headphones on when you're recording - your microphone is not hearing the same thing as your ears.

Upload the [ambiences HERE](https://drive.google.com/drive/folders/1XXksB3K-gd4bvjjHaI2mrUX56F-DtDz0?usp=sharing)

When you're done, upload to freesound.org and send me a screenshot as proof!


WEEK 5 ASSIGNMENT- 2D reskin part 2
------
Your creative director applauds your efforts, but wants to go in a different direction for the audio design.  

They've provided you with a small sample library [HERE](https://drive.google.com/open?id=1LxXc1ixDTijv4gvZXaD3z0bOhZrTRxaS)

You can do whatever you want with these samples, but cannot use audio from anywhere else!

When you are done, please upload [BUILDS HERE](https://drive.google.com/drive/folders/14NyAmWubZjkdUds-PBlc4JpzMyC3VUrq?usp=sharing)

(windows preferred, but if it's a mac build, please also bring it to class on a thumb drive)

In the interest of making sure we can run everyone's game in class, please also create a zip file of your Unity PROJECT and [upload it here when you're done](https://drive.google.com/open?id=1scI-AOAu-PebMLfZIr_bDCxzQ00t9KKR)

WEEK 4 ASSIGNMENT- 2D reskin part 1
------

**UPDATE**
[Follow these steps when making your builds!!](https://docs.google.com/document/d/1VW6J1PAGQ95Zgd4jGxv8iF7ky8drfmOyX_tu4tuZ7aU/edit?usp=sharing)

[And upload your builds here](https://drive.google.com/open?id=167_QmXOE5cQlvM6wFhkgCvFcQcKmcNwt)


Gabe Cuzzillo's wonderful game "Block Dog" is silent.  Clone this repo, open BlockDog in Unity Hub, and give it sound!

You should be working in the Scene called "Block Dog", on the Prefab called "AudioDirector."  Your work begins at the header **Game Sound Effects**. The minimum is to create **13** assets for the game.  2 of these are loops - the Danger Loop and the Background Music ( which could also be background ambience, if you prefer)

There's a [detailed rubric here,](https://docs.google.com/document/d/1WEAa_ZDIDO9yXnfGNs9Qb0WIcTCkBZF3NWzVn5RDqNk/edit?usp=sharing) but here are a few things to note:

You can use:
* Whatever you find on the internet, particularly from Freesound.org
* Anything using synthesis, if you wish.  Time permitting, I would like to cover a bit of synthesis during class, or I will post a tutorial
* Any effects.  If you have your own computer, try using vst plugins that you find on the internet.  For example, [this](https://glitchmachines.com/products/fracture/) is a free one that was used a lot on the game Prey, and can mangle sounds into something completely different!
* If you want to record sounds, you can do that too.

You are free to make any code modifications... as long as they only affect the audio!

You will lose points for:
* Any Unmodified samples.  You need to modify the sounds from freesound or the library in some way (changing pitch, layering, stretching, adding effects, etc).  The sounds in the library were not made for this game.  That's like serving a raw egg on a plate for dinner.
* Repeating sounds for different actions (for example, using the same sound for "jump" as for "throw").  I will frequently re-use *portions* of a sound in other sounds.  Again, treat your samples like ingredients.
* Sounds that start "late" - your sounds should have little to no silence (0.01 seconds max) at the beginning
* Sounds that end "early" - your sounds should always fade to silence.  
* Clicks/Pops in your loops

Aim for 
* clarity of actions (can I tell what's going on based on sound?) 
* consistency (does the sound seem to match with the visuals and animations?)
and, most importantly, 
* a sense of style/aesthetics (are the sounds augmenting the game in a way that gives it life and character?).

WEEK 3 ASSIGNMENT
------

[Choose 3 animated gifs from this directory](https://drive.google.com/open?id=1e_qmV88zN6YjvZpqyoVz3NtHGxD1qU3e).  If you want, you can find or make your own, provided that they are roughly 10 seconds long and contain a significant amount of motion.

Add sound effects to accompany the gifs.  The goal for the first pass is to have a distinct sound for every distinct motion - try to make it believable, as if the motions in the gif are producing the sound. 

Then, do a "second pass" on the same 3 gifs.  Choose different source material, and take a different artistic direction.  Try to still have sound for almost every motion. 

Render these (there should be 6 total) as WEBM videos and [upload them here when you're done](https://drive.google.com/open?id=1qxrEmKnZQF3m_l4I2pQ5KADMj9tTX0I5).  Make a sub-folder with your name.   

Also, if you need a refresher on how to render WEBM videos, I posted [this Tutorial](https://docs.google.com/document/d/1lyYhiCBcFWdTNpawd_aliKmS_cWuGRJJSpvW0MW8tvg/edit?usp=sharing) on the Wiki.

WEEK 2 ASSIGNMENT
------
Sound Collage - Create a 3-5 minute “story” (whatever that means to you) using samples sourced from Freesound.org (or elsewhere).  Experiment with different sample rates, bit depths, and sculpting using filters and EQ.

[Here's a quick tutorial on Rendering Reaper files](https://docs.google.com/document/d/1u33zeQcDhvRbJRrL2OdQ2vjyfth48lWYiCvT-erEX1c/edit?usp=sharing) 

Post your sound files [HERE](https://drive.google.com/open?id=1HrcL6GNCJ9EXvec5hhoKvY6lRSxjHI8s) when you are done.

WEEK 1 ASSIGNMENTS
------

Reading - [Seeing Circles Signs & Signals: “Intro, Signals & Sound” & “Sines & Sampling”](https://jackschaedler.github.io/circles-sines-signals/); please finish reading by Thursday, September 13

Critical Listening - working with a partner, play a game from the provided list, [available here](https://docs.google.com/spreadsheets/d/1Bn5J00GZ341uofRyMuntdbZQDFrSYf6LfDXIevePOCA/edit?usp=sharing), or make a case for a different game (do NOT watch a Let’s Play).  How would you describe the overall audio aesthetic, mood, or feeling of the game?  What do you think are the most important sounds, and why?  How do sounds change in response to player action (some sounds may be randomized, or connected to in-game physics in interesting ways)?  Look up some facts about the creation of the game audio - what’s interesting about the techniques or technology used?

You will be presenting a brief (~7 minute) presentation, at some point in the semester.  Schedule a week using the above list.  I'd prefer to only have one presentation per week.  They can be as early as Week 2 (September 12) if you want to get it out of the way.  It's a pass-fail assignment, and shouldn't take very long to complete.

For next class, bring in 3 sounds from a game project that you’ve worked on.  If you haven't worked on a game project, find a few short (20 seconds or less) sounds on Freesound.org
