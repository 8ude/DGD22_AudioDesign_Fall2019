# DGD22_AudioDesign_Fall2019
Fall 2019 Section of Audio Design for Games at LIU Post

See the [Wiki](https://github.com/8ude/DGD22_AudioDesign_Fall2019/wiki) for Helpful Links and slides from past weeks

You can find the [Syllabus here](https://github.com/8ude/DGD22_AudioDesign_Fall2019/blob/master/2019_Fall_CoreyBertelsen_AudioDesignForGamesDGD22%20001.pdf)

WEEK 4 ASSIGNMENT- 2D reskin part 1
------
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
