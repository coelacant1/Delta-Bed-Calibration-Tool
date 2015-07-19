# Delta-Bed-Calibration-Tool
This repository is set for the bed leveling calibration tool that I made for delta 3d printers.

TLDR: This is a program which can calibrate delta printers from an inaccuracy of +/- 2mm(Or more! But this was the test value) to an accuracy of +/- 0.015mm in under five minutes.

//What

I very recently started to work on a program which will calibrate a delta style 3d printer, which I made to calibrate my Rostock Max V2. This calibrates my printer without even touching the endstops, shimming the bed, or any other interesting method with which I have seen. But with recent tests, I have corrected my printer from a tolerance of about +/- 2mm (yeah, it was far off) to a tolerance of approximately +/- 0.015 in less than five minutes.

This program corrects the Alpha Rotation, Delta Radius, XYZ offset, Horizontal Radius, and Diagonal Rod length. I will go into more detail on how this is done later on in this later on in the post.

Based on tests:

This WILL calibrate your printer even if your towers aren't square.
This WILL calibrate your printer even if your endstops are off, in fact, you won't even need to touch your endstops with this calibration.
This WILL calibrate your printer even if your borosillicate plate is warped.
This WILL calibrate your printer if your bed is slanted.

//Why

I created this program out of frustration, mostly. I have tried using an uncountable amount of techniques others have used to successfully calibrate their printers. Most of the time, the techniques are very similar to how the Rostock Max V2 manual says to calibrate the machine, however, that method is flawed in some cases. It completely ignores the virtual towers of the delta machine it self. As the ideal delta machine would have six towers, however it isn't necessary as you can see when you look at your three tower delta. What I am saying, is that the manual only calibrates 3 of the 6 "towers" that need calibrated. In some cases, this works perfectly, on a perfect machine, however, the method in the manual cannot calibrate printers that are not mechanically perfect.

That is where this program comes into play, as I have tried to manually calibrate delta radius, tower offset, alpha rotation, etc. But it is next to impossible to get it perfect without spending 4 weeks straight, skipping your job and skipping sleep to calibrate it this way. But this program fixes these issues, and it doesn't take four weeks. I managed to calibrate mine to the precision that is SeeMeCNC states that the Rostock Max V2 can operate at in under 5 minutes.

Of course there are requirements to use this:
-calipers, or a dial gauge
-zip ties, or other way to mount the calipers/dial gauge
-rubber band if you are using calipers

Or if you are fancy, then you can use a Z-probe.

//How if you don't have a Z-probe.

I attached my calipers with 2 zip ties to the side of my hot end, and attached a rubber band to the slider on the calipers and the other end of the band to the hot end as well, this just allows the calipers to have a slight amount of pressure against the plate, giving more accurate results.

Once you're set up to measure the plate, enter this g-code after setting all of your EEPROM configuration back to it's original settings:

//How it works

To give a brief explanation of how this program works, I will describe it step by step.

/* The program in its current state has the ability to calculate your steps per millimeter for your printer if you do not know it(you Really should however), but it is needed to accurately changed the XYZ offset. */

//XYZ offset

The XYZ offset in this program only corrects the tower that is off by the largest amount, so if you measure one of the six points and they are significantly off, it will offset the height for that point setting it close to zero, while slightly offsetting the other towers. 

This step is not entirely necessary but it allows for the diagonal rod to be calibrated correctly. In the tests that I have done, when it is disabled, my diagonal rod is changed to 271.4. But when this is enabled, my diagonal rod is left at its base value of 269 (it is changed but only increased by 0.000374.)

//HorizontalRadius

The program simply averages the heights of the build plate at the towers and virtual towers at the edge of the build plate. This value is then subtracted by each tower to give the new tower heights, and multiplied by -0.5 then added to the horizontal radius. This simply balances the points around zero.

//Delta Radius

The next step was to calculate the delta radius. This involves finding the amount of change, for instance when you change the Delta Radius for the X tower from 0 to 1, it increases the virtual tower opposite of the X tower by 45%, of what the X tower is increased by and the two virtual towers adjacent (YOpp, ZOpp) to the X tower are increased by 27.5%, of what the X tower was increased by. The X delta radius does not however affect the Y and Z tower.

Once the program has these values, it can then predict where the Delta Radius values need to be for the XYZ towers (excluding the virtual towers) to be equal to approximately 0. The prediction is done in this case via linear regression.

//Alpha Rotation

Now, having all non-virtual towers calibrated, the opposing towers need calibrated. This is done partially via alpha rotation.

For instance, when you increase the base value of A by 210 to 211, it increases the virtual tower opposite of the Z tower by 0.5 and decreases the virtual tower opposite of the Y tower by 0.5. Alpha rotation does Not affect the opposite tower of the tower changed or any of the non-virtual towers. Given this, the virtual towers can all be balanced, i.e. the values will all be equivalent but greater than or less than zero (unless you are not from this planet.)

//Diagonal Rod

To give an example of what happens when you change the length of the diagonal rod, if you increase the base value, in my case 269 to 270, the XYZ towers will be increased by approximately 13% of the value changed (1, so 0.13mm) and the virtual towers will be increased by 21% (0.21mm) of the value.

This presents an issue, but nothing that can't be fixed! Given the situation, you could either increase or decrease the value of the diagonal rod until all values are equivalent, but then all values will be greater than or less than 0. But since these heights are all the same it can be canceled out by changing the value of the horizontal radius until all values are approximately zero.

Getting through these steps, it is possible to get a theoretical tolerance of +/- 0.0001mm(the programs settings), giving a perfectly flat bed. (Even if your bed was mechanically warped.)


Following these steps, it is entirely possible to calibrate a delta with only modifying values in the EEPROM. That is what the program does for you! Except all you have to do is enter your current settings and then get your new values right back.

And if your calibration wasn't close enough the first time, you can go through again! But that would only apply if you were planning on printing something 1/10th the thickness of a piece of paper.

//How to use it

Enter in your current settings, then press calibrate, then copy the change data back into your EEPROM.

Since this is not a compiled program (just Javascript, sorry I am lazy), I am making it open-source, anyone can improve it and modify it to their need.
