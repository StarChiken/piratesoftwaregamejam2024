# Clean Code - Uncle Bob / Lesson 1
# https://www.youtube.com/watch/7EmboKQH8lM

00:00:00.000 for such an ambition we need the right skills and as developers we  
00:00:27.270 code so let's sharpen those skills and start coding a better world together  
00:01:18.240 this first edition of coding a better world together is on clean coding and clean architecture
00:01:28.440 and who better to teach us more about this than mr. clean code  
00:01:33.120 aka Uncle Bob the one and only Robert see Marquis  
00:01:50.360 and here are your hosts for today Ahrens clubbers and fedoras mice the next which  
00:02:04.100 will be Uncle Bob I will introduce him Uncle Bob has an impressive career in the IT industry he  
00:02:11.960 has been working there for more than 40 years he always had a keen eye for what makes a developer  
00:02:18.680 a good developer he shares his fuse as often as possible and he will do that here in these  
00:02:29.210 coming two days also he is one of the authors of the agile manifesto and the manifesto for  
00:02:38.960 software craftsmanship he hates it that that agile movement has been taken hostage by the  
00:02:46.850 consultants and conference organizers with that they abandoned the programmers and by  
00:02:55.010 abandoning the programmers they abandoned the values and discipline of craftsmanship and I  
00:03:03.500 think that's a very very sad message but Uncle Bob stays positive and he keeps sending out that  
00:03:11.900 message of craftsmanship so please welcome Robert C Martin better known as Uncle Bob
00:03:40.400 okay hello I'm Bob and by the way thanks to everybody who brought me in here that was  
00:03:48.650 awfully nice of you it's a pleasure to be here thanks to rob a bank and everybody who who made  
00:03:54.410 this possible special thanks to shirt from wanna flex she was kind of the catalyst for this entire  
00:04:00.890 event how many of you are programmers see you are my tribe I don't care if you're young or  
00:04:14.510 you're old or black or white or a man or a woman I don't care who you like or who you love if you  
00:04:23.780 are a programmer you are part of my tribe you and I and we all together share a passion for  
00:04:34.160 something and we can communicate it about it in a way that most other people can't and so that's  
00:04:41.720 something that we should cherish together how far away is the Sun five why do people know this and  
00:04:57.440 and it's as in it's a strange thing tonight I said five minutes you meant five light minutes  
00:05:03.170 the actual number is eight light minutes but why would you care how many light minutes away  
00:05:08.630 the Sun is does someone know it in kilometers or miles how many how many kilometers away is  
00:05:15.410 the Sun good 150 million kilometers that's a more human measurement than eight light minutes good  
00:05:23.270 of course if somebody is eventually going to say it's one astronomical unit and thank you  
00:05:27.530 for that how do we know that the Sun is 150 million kilometers away somebody read it in  
00:05:41.930 a book or something is it so who is the first person to calculate the distance to the Sun
00:05:49.970 this person lived 2,500 years ago his name was Aristarchus and let me show you the technique he  
00:05:58.340 used because it's a fascinating technique oh my goodness I get a couch to sit on in everything  
00:06:05.870 hmm now let's see here's the Sun here is the moon and here's the earth and this line right  
00:06:20.480 here is the line that divides I should probably do that the other way I shouldn't the the line  
00:06:25.820 that divides the night side of the moon from the day side of the moon now if you are standing on  
00:06:32.540 the earth and you are looking at the moon and you see that the moon is exactly half phase then you  
00:06:39.530 know that this angle is a right angle if you could measure this distance the distance from  
00:06:46.820 the earth to the moon and if you could measure this angle right here then you could calculate  
00:06:53.180 the distance to the Sun because you'd be trying to calculate that side of the triangle now this  
00:06:58.760 angle here we can measure because you can get outside and see the moon in the sky and the Sun  
00:07:04.910 at the sky at the same time she might as well measure that angle and then the problem is to  
00:07:10.370 measure this distance here the distance from the earth to the moon we'll come to that in a minute  
00:07:15.860 Aristarchus did these initial measurements and he got the angle measurement off by a factor of 10  
00:07:22.370 which made his whole his whole calculation off by a factor of 10 although that's actually not  
00:07:28.160 bad because you can imagine 2500 years ago he didn't have an awful lot of instrumentation to  
00:07:33.740 measure that angle and how do you measure that angle you have to find the exact center of the  
00:07:37.910 moon and then you have to find the exact center of the Sun and it's difficult to stare at the Sun  
00:07:43.340 for any length of time to find the exact center and then you have to measure the angle and it's  
00:07:48.140 a very very tiny angle it's actually a very close to 90 degrees the difference from 90 degrees is  
00:07:54.110 about point 3 degrees and he couldn't measure that that precisely so he actually measured 3 degree  
00:08:00.380 difference he thought it was 87 degrees but then he went ahead and did the rest of the mass now  
00:08:05.650 how do you find the distance to the moon well he reasoned this way here's the Sun here's the earth  
00:08:14.830 here's the moon at the moment of lunar eclipse the shadow of the earth is covering the moon  
00:08:23.830 the shadow of the earth is roughly the diameter of the earth the size of that shadow is roughly  
00:08:30.850 the diameter of the earth how long does the moon spend in that shadow well he measured that time  
00:08:37.570 to be about three hours how long does it take the moon to make a complete circuit of the earth well  
00:08:43.270 that's close to 28 days so that gives you a nice ratio it is the ratio between this the Earth's  
00:08:49.570 diameter and the circumference of the moon's orbit from which you can calculate the radius of that  
00:08:55.390 circle and get the distance to the moon in earth diameters so now we need to know the size of the  
00:09:03.430 earth now Aristarchus did not know the size of the earth and he didn't bother to calculated it was  
00:09:08.950 another Greek who calculated the size of the earth and his name was Eratosthenes about 30 years later  
00:09:15.220 and he reasoned it this way he said all right look here's the earth here's the Sun and a stick  
00:09:22.840 in Alexandria Egypt on the first day of summer at noon will not cast any shadow the Sun is perfectly  
00:09:32.020 overhead but at the exact same moment 500 miles away in the Egyptian town of Memphis the stick  
00:09:40.330 casts a shadow of seven degrees he measured the distance by timing the trip on a camel
00:09:46.330 now it's pretty easy to do the math 360 divided by seven times 500 and you get the circumference  
00:09:55.420 of the earth you divide that by PI you get the diameter of the earth you plug that into the  
00:10:00.370 lunar equation to get the distance to the moon you plug that into the solar equation to get  
00:10:05.230 the distance to the Sun and Aristarchus calculated that the Sun was 10 million miles away 15 million  
00:10:13.000 kilometers away so he was off by a factor of 10 that's not bad for some guy 2500 years ago  
00:10:19.170 without any instrumentation that is the power of human reasoning he just studied the situation long  
00:10:25.770 enough and gathered enough data and came up with a pretty good answer his answer his solution to the  
00:10:31.500 distance of the Sun was not battered until 1630 so for 2,000 years that was the best measurement  
00:10:38.730 we had of the distance to the Sun and even back then it set the scale for the solar system in a  
00:10:45.090 way that nobody had expected of course this is not what we're supposed to be talking about is  
00:10:50.730 it right we're supposed to be talking about clean code so let's talk about clean code
00:11:09.870 this talk is clean code and I have you I believe for for two 90-minute sessions on this topic so  
00:11:16.500 we ought to be able to cover a fair bit of ground here and I'm not going to go through that I will  
00:11:21.600 go through this this is a cartoon I found in a magazine several years ago and I thought it was  
00:11:28.380 particularly poignant the only valid measurement of code quality is WTFs per minute coming out from  
00:11:38.670 behind the door where the code review is taking place and you look at the code the door on the  
00:11:43.290 left and you realize that's probably pretty good code because there's not that many WTFs coming  
00:11:48.030 out from behind that door on the other hand that door on the right must have some pretty bad code  
00:11:53.220 behind it because there's a whole bunch of WTFs coming out from behind that door what we are going  
00:11:59.130 to be talking about over the next several hours is how to get behind that door on the Left how  
00:12:06.240 are we going to make our code good enough to survive a code review such that our peers are  
00:12:14.490 not tearing their hair out trying to figure out what the hell our code does why is this important
00:12:27.530 how many computers are in this room right now thousands good answer how many computers do you  
00:12:40.850 have on your body at the moment now I can do this count with me I have my iPhone how many processors  
00:12:49.520 are in here eight you think is it eight I don't know how many it is there's the main processor  
00:12:56.990 in the screen processor and the Wi-Fi processor in the GPS processor in the Bluetooth processor  
00:13:02.090 it's got to be a bunch of others I don't know there's a bunch of processors sitting in here I  
00:13:06.350 am holding in my hand an amount of computing power that exceeds the computing power of the world in  
00:13:13.580 1980 which is pretty interesting and you know we all have one and what do we do with it we play  
00:13:21.170 Angry Birds right which if you think about it it's pretty cool Angry Birds that's a great program  
00:13:26.870 virtual reality and everything right on the screen got some more processors on me let's see I've got  
00:13:35.120 my earbuds hmm how many computers are in here well there's one for each air but and the case has got  
00:13:43.580 a computer there's least three in here what else do I have on me I've got this microphone thing you  
00:13:48.770 think there's some processors in there I think probably it's probably a processor in there so  
00:13:55.310 on my body I have several dozen computers just working away helping me give a talk now look  
00:14:04.460 around the room how many computers are in the walls and the ceiling this cameras up there I got  
00:14:14.600 to have processors in them there's these lights here I imagine there's processors in those because  
00:14:18.890 looks like they're robotic and oh Jesus there's stuff on the ceiling I don't know what it is if  
00:14:26.420 probably processors in it there's the speaker's the speakers have computers in them nowadays it is  
00:14:34.010 cheaper to put a little digital signal processor in a speaker or a microphone and filter out 50  
00:14:39.170 cycle hum than it is putting an inductor and a capacitor in there and that digital signal  
00:14:43.360 processors got some C code that does fast Fourier transforms and is written by some 22 year old at  
00:14:48.910 3:00 in the morning got speakers and microphones got smoke detectors get signs yet exit sign there  
00:14:59.530 there a processor in that exit sign does it have a battery in it is that battery being charged is  
00:15:07.840 there a little processor sitting there monitoring the trickle charge going into that battery
00:15:12.160 how often do you interact with a software system no no not you how often does your  
00:15:23.230 grandmother interact with a software system if you have a grandmother but how often in  
00:15:31.120 our society today it is impossible to go more than 60 seconds without interacting with the  
00:15:37.720 software system because there's nothing you can do in our society without interacting with the  
00:15:42.070 software system you can't talk on the phone you can't watch TV you can't microwave popcorn you  
00:15:47.860 can't wash the dishes or your clothes you can't dry your clothes you can't drive anywhere without  
00:15:53.530 interacting with the software system how much code is in a modern car how many lines of code  
00:16:00.700 in a knot a Tesla a Tesla's way over the top just a normal old modern car how many lines of code are  
00:16:07.330 in that it's over 100 million you get into your car you start to drive there's a hundred million  
00:16:14.590 lines of code executing in that car most of it of course is in the entertainment system and the  
00:16:21.040 GPS system and all that nonsense but there's a fair bit of code running in the engine when  
00:16:29.590 you put your foot on the brake do you believe that there's a cable that runs from the brake  
00:16:37.540 pedal to the calipers that squeeze on the disk or do you realize that there are if statements  
00:16:45.430 in the way and who wrote those if statements if statements that are going to decide whether or  
00:16:55.840 not to stop the car when you push on the brake how many people have died because of failures  
00:17:04.390 in those if statements dozens there are dozens of people who have died in automobile accidents  
00:17:10.270 because the software that controlled the brakes in the accelerator failed for some stupid reason  
00:17:16.030 you and I are killing people now we didn't get into this business to kill people most of us  
00:17:23.770 are programmers because one day we walked into a store and we saw some dumb computer on a rack  
00:17:28.329 and we typed five lines of basic to put our our a print statement that printed our name into an  
00:17:33.760 infinite loop and we saw our name going up the screen or what yeah that's what I want to do for  
00:17:39.430 the rest of my life but now we're killing people we didn't want to get into a business where you  
00:17:47.770 could do that not only can you kill people with software you can lose massive fortunes  
00:17:53.230 who knows about night capital who's heard about the fiasco at night capital about six years ago  
00:17:58.240 right I'm not going to go into all the details just some some guy made one dumb mistake and it  
00:18:05.200 was just a little dumb mistake but he managed to lose 450 million dollars and 45 minutes
00:18:10.180 our society runs on software nothing happens in our society without software nowadays didn't  
00:18:24.520 used to be that way wasn't that way in the 1960s wasn't that way in the 70s wasn't that way in the  
00:18:29.440 80s but today our society runs on software there is nothing you can do without software you cannot  
00:18:36.700 buy anything you cannot sell anything you cannot pass a law you cannot enforce a law you can't get  
00:18:43.030 insurance policies you can't get claims from insurance policies you can't get money out of  
00:18:47.980 the bank without software software runs everything and our society does not yet realize just how deep  
00:19:01.780 engine it is on software you and I probably don't realize it very well just how dependent everything  
00:19:10.510 is on software and who writes that software we do we rule the world other people think they rule the  
00:19:23.770 world then they hand those rules to us and we write the rules that run in the machines that  
00:19:31.540 govern everything we need to talk to people about taxes now since we rule the world seems fair that  
00:19:40.840 we shouldn't have to pay any taxes but we'll come back to that later does society understand just  
00:19:51.730 how dependent it has become on us answer that is no not yet although there have been hints there  
00:19:59.500 have been moments when we thought maybe society would wake up to this fact the events that will  
00:20:05.620 eventually of course cause society to wake up is when some poor programmer does one dumb thing in  
00:20:11.020 and kills 10,000 people at a shop and it's not hard to imagine what that would be all right  
00:20:16.630 you think for a few minutes you'll realize that it's about seven dozen ways you could kill ten  
00:20:20.650 thousand people with a little software failure and when that occurs and you know it's going to occur  
00:20:25.750 at some point in time some poor software idiot is going to do some dumb thing and kill 10,000  
00:20:32.110 people and when that occurs the politicians of the world will rise up in righteous indignation  
00:20:37.300 as they should and they will point their finger right at us and you might like to think oh no  
00:20:42.850 they're not gonna point their finger at me I'll point my finger at their finger at my boss or my  
00:20:47.080 company cuz it's not me but we saw what happened when the CEO of Volkswagen North America testified  
00:20:56.470 before the American Congress about why the software in Volkswagens was cheating the  
00:21:02.260 Environmental Protection machines in California and the Congress asked that CEO how could you  
00:21:09.010 have let this happen and the CEO answered and I quote it was just a couple of software developers  
00:21:17.200 who did this for whatever reason unquote now he was right it was a couple of software developers  
00:21:29.020 who did it although it was not for some reason they knew exactly why they were doing it by the  
00:21:33.490 way those software developers are in jail now as they should be so when the event occurs and  
00:21:42.010 the politicians are eyes up and say we got to do something about these programmers they're out of  
00:21:46.690 control and they point their fingers at us and they say okay how could you have let this happen  
00:21:52.360 we'd better have an answer for him because if our answer is you know my boss told me we had to get  
00:21:57.850 it done by Tuesday if that is our answer and the politicians of the world will hang their heads in  
00:22:04.060 disappointment and shame and they will and disgust and they will do the one thing that we don't ever  
00:22:10.060 want them to do they will legislate they will regulate they will tell us what languages we  
00:22:18.370 can use and what platforms we have to write on and what courses we have to take and what books  
00:22:23.770 we have to read what processes we have to follow and what signatures we have to get and we will all  
00:22:29.050 become civil servants I would like to avoid this so how do you avoid it well you get there first  
00:22:37.270 you get there first by establishing the ethics of software development what is our ethics do  
00:22:46.150 we have a stated set of ethics do we have a set of standards a set of moral standards that all  
00:22:52.600 programmers follow do programmers take an oath to uphold a set of standards a set of ethics we  
00:22:59.800 don't have that we don't have a profession because of that because in order to have a profession you  
00:23:06.040 have to have something you profess and we don't profess anything at this point in time we're going  
00:23:12.880 to have to come up with this we're going to have to learn to adopt a set of standards and ethics so  
00:23:18.040 that when the politicians of the world do point at us and they will and they say how could you have  
00:23:23.260 let this happen we can respond by saying look this was a horrible accent and we regret it but it was  
00:23:31.480 not due to our negligence and we can prove it was not our negligence because here are the standards  
00:23:37.180 that we enforce here are the morals that we adhere to these are the ethics that we claim to profess  
00:23:44.410 and if we can say that to the politicians of the world then when the politicians of the world  
00:23:51.730 decide to regulate us they will know how to do it because they will simply take the rules that we've  
00:23:56.740 already invented and turned them into law this is what's happened to doctors it's what happened  
00:24:02.050 to lawyers it's what happened to architects so it happened to everybody it's going to have to happen  
00:24:05.770 to us as well and so you and I have to begin this thought process on what our ethics is what do we  
00:24:16.240 value what is it that a software developer holds dear and one of those things had better be the  
00:24:26.440 cleanliness of code so let's talk about that now I've got a few things here I'm just gonna quickly  
00:24:41.290 go over them here's a fellow his name is Kent Beck has anybody heard of Kent Beck several of you good  
00:24:47.710 all right the rest of you should have heard of him by now so do a little bit of research on  
00:24:51.640 Kent Beck he's one of the leaders in the field he wrote a book some time ago called implementation  
00:24:58.690 patterns that was the name of the book and in that book he said that the book was in the introduction  
00:25:04.390 of the book he said that the book was based on a fragile premise and the fragile premise was  
00:25:11.350 that good code matters and when I read that in his book I scratched my head and I thought why  
00:25:18.310 would Kent Beck have said that that was a fragile premise it seems to me that it's a very rock-solid  
00:25:25.540 premise but yes good code matters and it matters for a whole bunch of very powerful reasons so for  
00:25:31.750 example why are we so slow why our programmers so slow ooh here has worked on a greenfield project  
00:25:51.970 a project where there's no code ah bunch of you how fast can you go those first few days where  
00:26:02.380 there's no code and someone comes to you and says can you get a feature done and you think yes you  
00:26:10.720 start writing code code pours out of every orifice of your body and you get that feature working and  
00:26:17.770 everybody goes whoa you got that working fast it only took you a few days we're programmers can you  
00:26:25.090 do it again yes come back to that team a year later can you get a feature working for us hmm  
00:26:38.080 tricky probably gonna take us six months he used to be able to do that in a couple of days yeah but  
00:26:47.050 you don't know how messy this system has become why if we touch even one line of code all hell  
00:26:57.310 could break loose why'd that happen here's the thing that happens to software teams right they  
00:27:06.640 start out fast they start out with a beautiful design and they start out lovely in there fast  
00:27:11.350 and they write features and everything's working great but they make a mess because they want to  
00:27:19.210 go fast they make a mess and as they make a mess as the mess builds the team gets slower and slower  
00:27:26.080 and slower until they bottomed out at 1% of their original productivity and there's a but what are  
00:27:34.270 you going to do about this let's say you're a manager right and you've got his team this  
00:27:38.350 team's been working on this software for two years now and they can't get anything done no feature  
00:27:43.600 can get done in less than you know six months and even then it's going to be late and it won't work  
00:27:47.560 what do you know is the real world for managers what are you going to do as a man if you've made  
00:27:54.780 promises to people you have you've set plans out there there's people expecting features and the  
00:28:00.660 teams cannot deliver those features what are you going to do as a manager what would your option  
00:28:06.390 be you got to go fast somehow add more people right that's what you do you have to double the  
00:28:15.900 staff everyone knows you go twice as fast if you double it and you're laughing why are you laughing  
00:28:21.360 because you know this is nonsense you can't go faster by doubling the staff adding more  
00:28:26.580 people does not make you go faster what does it do the moment you decide to add new people to a  
00:28:32.250 team what happens to that team it slows down why because the new people suck the life out of the  
00:28:38.550 old people for months now you're kind of hoping that those new people will get smart after a while  
00:28:48.420 and and then productivity will rise but there's another effect that kicks in who's training the  
00:28:54.300 new people the people who made the mess in the first place and in fact it's not the old people  
00:29:01.860 training the new people it is the old code that trains the new people the new people are thrown  
00:29:07.650 into the fire they've got to make some sense out of this system they read the old code they say  
00:29:12.660 to themselves oh I see how things are done around here they emulate it of course and just continue  
00:29:20.190 to make the prospect problem worse and the code gets Messier and Messier no matter how many people  
00:29:26.430 you add to the team and nothing you do can make that productivity rise that's why programmers are  
00:29:36.150 slow and slow because they make a mess if they didn't make a mess they'd be fast if you could  
00:29:43.920 keep the code clean it wouldn't be a mess you could add new features they would get added in  
00:29:52.200 a reasonable amount of time as long as you could continue to keep the code orderly and clean we're  
00:29:59.880 going to talk about a number of strategies for doing that but I wanted to impress that point on  
00:30:03.690 you pretty thoroughly we go slow because we make messes why do we make the mess what drives us to  
00:30:12.750 make the mess in the first place the desire to go fast we make the mess because we think I got  
00:30:18.870 to get done quickly they're expecting me to get a lot of stuff done I got it I got to just get it  
00:30:23.100 done oh it worked how many of you have done this it's hard to get code to work you struggle to make  
00:30:33.060 code work so you're working on the code and you're working on the code and you're trying to get it to  
00:30:37.380 work it's not working and you're sitting in the debugger and you're single stepping through it  
00:30:40.950 and you're trying to make it work in and then all of a sudden it works Sookie don't anybody breathe  
00:30:46.200 move carefully and I'll check it in Co God thank you that's the wrong thing to do the fact that  
00:30:59.730 you got it working is only half the job once the code works that's when you have to clean it no  
00:31:07.020 one writes clean code first nobody does because it's just too hard to get code to work so once  
00:31:14.340 the code works it will be a mess human beings do not think in Nice straight lines they don't think  
00:31:22.020 in if statements and while loops they cannot foresee the entire algorithm so we piece the  
00:31:27.570 thing together we cobble it together with wire and scotch tape and then it suddenly work so we're not  
00:31:33.420 quite sure why and that's the moment when you say all right now I need to clean it how much time do  
00:31:41.700 you invest in cleaning it roughly the same amount of time it took you to write it and that's the  
00:31:47.760 problem nobody wants to put that effort in because they think they're done when it works you're not  
00:31:53.610 done when it works you're done when it's right and if you adopted that attitude well then the  
00:32:02.040 code would stay clean and you would never go through the slow down more to say about this later
00:32:12.130 you have all these slides I never actually used them they tell the whole story but I just kind  
00:32:19.900 of wing it anyway dad oh damn boom boom oh here's a good one let me stop there memorize  
00:32:28.060 this one the only way to go fast is to go well somebody said today you know twice as fast twice  
00:32:34.630 as proud okay fine how do you go fast you go well twice as well twice as proud we might go fast too
00:32:42.400 the only way to go fast is to do a good job if you do a lousy job you're going to go slow donna  
00:32:53.620 diva what is clean code alright who knows who this guy is Grady Booch what a great name if your name  
00:33:03.940 is Grady Booch everybody knows your name because who wouldn't know that name well that's not granny  
00:33:07.840 boots that's yarnís Tristram sorry I picked up back did the wrong side this is your nest rooster  
00:33:13.450 who knows who that is he's Danish does that matter I guess not okay so yeah mr. strip and who is he  
00:33:21.610 C++ he made the language C++ that's correct so I wrote to yarn I said Jana what does clean code  
00:33:31.300 make mean to you and he said and this is this is very typical of Jana stirs drip he said I like my  
00:33:38.980 code to be elegant and efficient and then he said this clean code does one thing well one  
00:33:47.050 thing well now this one thing idea has been around in software for 40 years or more people are always  
00:33:55.510 writing about how a function should do one thing and one thing well it's a very very old idea and  
00:34:01.600 strip echos it here but what does one thing mean it seems to be a kind of subjective subjective  
00:34:12.850 measure what does it mean for a function to do one thing well I think I know the answer to this and  
00:34:18.790 I'll tell you a little bit later I think I have a completely objective way to measure one thing  
00:34:25.520 and if you if you adhere to that objective way it transforms the structure of your code remarkably
00:34:32.000 there's Grady Booch it was in there somewhere Grady Booch the chief scientist at rationale  
00:34:46.340 he wrote a book in 1988 called object-oriented software design with applications anybody read  
00:34:54.530 that book 1980s late eighties do we have any baby who was born then yeah okay good all right so  
00:35:03.500 that's fine that fascinating book very early book on object-oriented design Grady Booch is also the  
00:35:10.130 first person I believe to have the title chief scientist I think he invented that and rational  
00:35:17.450 said yeah you can be the chief scientist that's fine and ever since then everybody's wanted to  
00:35:21.740 be a chief scientist at some company somewhere so I asked Grady you know Grady what's clean  
00:35:26.450 code he said clean code is simple and direct fine and then he said this clean code reads  
00:35:36.140 like well-written prose have you ever read code that read like well-written prose what does that  
00:35:51.980 mean well written prose now I think I know what that means and I think I can show you what that  
00:35:59.210 means as we go along I think I can tell you what code that reads like well written prose  
00:36:04.730 means and let's go to Michael feathers Michael feathers I wrote to Michael I said hey Michael  
00:36:15.470 what's clean code and he said clean code always looks like it was written by someone who cares  
00:36:22.160 what a lovely statement that's a good attitude right there clean code always looks like it was  
00:36:29.810 written by someone who cares when's the last time you read a module and your thought as you  
00:36:37.550 are reading module was the author cared about me the author cared about everybody else who  
00:36:47.330 is going to be reading that code when's the last time you had that experience the author of this  
00:36:54.020 code cared about me very interesting and by the way it brings up a fascinating point what's your  
00:37:05.270 job you may think that your job is to get code to work that's not your job that's only half of  
00:37:11.870 your job and it's the least important half of your job the more important part of your job is  
00:37:19.430 that you must write code that other people can maintain and use and make work if you hand me  
00:37:31.520 code that works perfectly but I can't understand it then as soon as the requirements change that  
00:37:37.880 code is useless on the other hand if you give me code that does not work but I can understand  
00:37:43.670 it I can make it work it is much more important that your peers be able to understand the code  
00:37:49.940 you write then that the computer can understand the code that you wrote it is more important to  
00:37:55.820 communicate with your peers using a programming language than it is to communicate with the  
00:38:01.310 computer because if you do that well somebody will make it work the last one word Cunningham  
00:38:12.020 who knows who worked Cunningham is somebody heard the name you know what that isn't few of you do I  
00:38:19.250 said know you'll know who he is as soon as I tell you he's the guy who invented the wiki who's he's  
00:38:24.590 the wiki everybody puts their hand up ok good Ward Cunningham is the guy who invented the wiki  
00:38:31.310 he invented the wiki out of 19 lines of Perl in 1990 something or other put it up on the on the  
00:38:37.670 website by the way that very first wiki is still alive you can get to it if you want to the URL  
00:38:44.690 is C to calm the letter C and the digit to calm is the very first wiki ever it's still there and  
00:38:55.400 by the way it's full of fascinating stuff you'll see all kinds of design pattern stuff and extreme  
00:39:00.440 programming stuff and agile stuff because it was there at the beginning and I asked Ward  
00:39:06.080 Ward what's clean code and he said this you know you're working on clean code when each routine you  
00:39:15.140 read turns out to be pretty much what you expected when's the last time you read code and as you're  
00:39:25.160 reading it it was pretty much what you expected you reading along and you're going yeah yes yep  
00:39:32.660 yep yep yes mmhmm yep yep when's the last time you had that experience what he's saying is no  
00:39:45.140 WTF s per minute zero clean code is no surprises no WTF everything is pretty much what you expected  
00:39:54.200 that's good clean code and that's what we're gonna pursue over the next few hours now I want to go  
00:40:10.610 to a different part of this presentation let me just cycle up here now I've been preaching  
00:40:23.150 for a while now I think we've got a little bit of time left I think I would go till 10:45 if I  
00:40:31.280 remember correctly so we've got enough time for this that's good I am going to put some code on  
00:40:35.870 the screen it'll be three pages of code I will give you one minute per page the last page is  
00:40:42.620 half a page so you'll get two and a half minutes the code is Java if you're a c-sharp programmer  
00:40:48.140 you won't be able to tell the difference because they're the same language right read the code  
00:40:54.140 figure out what it does and then when we're done I'll ask you what it does and we will begin now
00:43:42.970 what that could do
00:43:48.030 what said a good it generates HTML and you knew that because the very last line was get HTML good  
00:43:57.450 what was the name of the function testable HTML good so we got the first line in the last line  
00:44:03.390 what the middle do so some of you may have gotten the idea let me let me just walk you through it  
00:44:14.400 I'm not going to walk you through the whole thing I'll just tell you what it does this is a little  
00:44:18.300 bit of code in a in a tool called Fitness fitness is a wiki some of you may have noticed that it was  
00:44:24.120 a wiki Fitness is also a testing tool it allows customers and QA people to define acceptance  
00:44:32.550 tests in the form of pages now if you are used to a testing environment you know that tests  
00:44:40.110 typically have setups and tear downs in this tool those setups and tear downs are on different pages  
00:44:47.100 so the job of this code is to take every test page and then find all of the setups that are  
00:44:54.210 appropriate and prepend them and find all the tear downs that are appropriate and append them and  
00:45:00.090 then take the whole thing and generate HTML and feed it into the testing engine that's what this  
00:45:05.520 is supposed to do so this function up ends setups and tear downs to a test page what was the name  
00:45:12.660 of the function testable HTML terrible name for a function by the way that's a noun you don't want  
00:45:19.020 nouns as function names function name should be verbs because functions do things now let's let's  
00:45:24.930 look at this function a little bit more there's quite a few bad things going on in here when we  
00:45:31.860 read a little bit here let's see we've got this nice thing right here that first line wiki page  
00:45:38.880 and if you're a programmer in this environment you see that and you kind of heave a sigh of  
00:45:44.280 relief because the wiki page is the highest level abstraction in this system it gives you a great  
00:45:49.860 comfort to know that you can recognize that first line the next line creates a string buffer so now  
00:45:56.280 we are going from the highest level concept in the system to one of the lowest level things in a Java  
00:46:01.890 program this is rude the programmer is being rude the programmer is taking you from the heights to  
00:46:10.110 the depths in the span of one line there is a fundamental rule for a function and the rule is  
00:46:16.590 that every line of a function should be at the same level of abstraction and that level should  
00:46:23.010 be one below the name what's happening here well look I mean heck he goes to the wiki page first  
00:46:30.600 and then to the string buffer so up and down and then he goes to a slightly high level thing  
00:46:36.090 that's the page attribute and then he's got an if statement another wiki page that's nice and  
00:46:41.070 high level page crawler that looks like it must be high level ooh there's a null check that's pretty  
00:46:46.680 low level isn't it you keep on reading down and you get to hear a dot is that dot important does  
00:46:57.570 that dot have to be there you're the programmer trying to understand this code what does the  
00:47:02.250 meaning of that dot we've gone from the highest level concepts to adopt this is route now why  
00:47:14.790 is the code written this way well the code is written this way because this is how you write  
00:47:19.170 code when you are writing code your brain does this oscillation from high to low level and it  
00:47:26.670 works like this you sit there and you're writing the code you think all right I'm gonna need a  
00:47:31.560 wiki page all right get the wiki page okay I'm gonna need a string buffer ok string buffer okay  
00:47:37.380 I better check something all right if statement oh it might be null check for now okay this is  
00:47:45.210 the way your brain works right as you're writing the code so as you're writing the code you are of  
00:47:49.770 course creating this code that goes up and down the abstraction levels which is rude not to you  
00:47:57.780 the author but to the reader the mistake that the author here made is that the author got all this  
00:48:05.730 working and then didn't fix it so let's continue we've got all this up and down stuff and you can  
00:48:14.100 kind of see that obviously I want you to focus though on this null check which is followed by  
00:48:19.980 a page crawler which is followed by three appends null check page crawler three appends oh look now  
00:48:31.650 check page crawler three appends now check page crawler four appends what's the extra pen who  
00:48:42.570 sent line in is that line end important that line end has to be there no check page crawler three  
00:48:55.860 appends this is very clear what the programmer did here the programmer got the first quarter of this  
00:49:01.590 working and then like any good programmer did a copy-paste and he fiddled that part into working  
00:49:06.630 copy paste he fiddled that into working copy paste he fiddled that into working perfectly normal way  
00:49:12.450 for a programmer to behave when they're trying to get it to work he just didn't go back and  
00:49:18.120 fix it he left it in this intermediate working state now by the way the offer very well could  
00:49:26.910 have been me it's not unlikely that I wrote this code although it's possible someone else did too  
00:49:32.700 so I'm not claiming authorship of it but I do claim that many years later I saw this code in  
00:49:39.180 fitness and thought analysis ugly I should fix it so I started to refactor it let me show you  
00:49:44.880 the an intermediate refactoring this is not the final refactoring it is just a refactoring that  
00:49:50.640 all was about halfway through I'll give you about ten seconds to look this over
00:50:03.540 what's that do first of all did anybody notice that the original function was  
00:50:20.430 completely surrounded by this if statement it was but it was kind of buried very hard to see  
00:50:27.690 in that original code here you can kind of see mmediately it's all about this if statement so  
00:50:32.190 most of this code doesn't get executed unless this is a test page take a look at that if statement  
00:50:38.340 the if statement fascinating because it uses a variable the variable is called is test page now  
00:50:44.880 is test page is defined right above it this is called an explanatory variable the only purpose  
00:50:50.970 for that variable is to explain what its contents is and the contents is what you would have put in  
00:50:58.770 the if statement page data that has attribute colon test well I think it reads a lot better  
00:51:03.930 to say if is test page this is one of the ways you get code to read like well-written prose you  
00:51:10.380 construct well-written prose from the names of variables and functions now if you read  
00:51:19.650 further we get the wiki page fine and we get the string buffer so we're still going up and down  
00:51:24.390 the abstraction hierarchy a little bit and that's that's still bad but okay after that we include  
00:51:29.010 the setup pages we append the content we include the teardown pages we set the content and convert  
00:51:34.470 it all to HTML it's not very hard to understand what this code is doing still some problems with  
00:51:42.240 it it's got a much better name render page with setups and tear downs that's better maybe it's  
00:51:48.330 not perfect maybe it should be rendered test page with set ups and tear downs but okay looks like  
00:51:53.130 a pretty good name reasonable structure things are getting better here why is this better what  
00:52:00.510 makes it better why is it easier to understand it's smaller that's the reason you don't need  
00:52:08.640 any other reason beside that it's smaller and if smaller is better well let's turn that knob  
00:52:14.220 up to 10 let's make it really small here let me show you where it finally ended up that's the  
00:52:21.930 final refactoring of this function render page was set up some tear downs if it's a test page  
00:52:28.380 include the set ups and tear downs converted to HTML takes you know time to understand this  
00:52:32.460 function this function is polite why is it polite what do I mean by polite so there's  
00:52:40.230 anybody studied journalism here you know how to write a paper the rules for writing an article  
00:52:45.510 or writing a paper are very very simple you start with the title the headline the title  
00:52:51.030 right then you have a paragraph usually called the synopsis or the abstract it's usually one  
00:52:58.740 paragraph and it describes everything in the in the paper in high-level terms and then the next  
00:53:03.750 paragraph down is slightly more detailed the paragraph after that is a little more detailed  
00:53:08.130 than that and as you read downwards the detail increases in to get to the bottom where there's  
00:53:13.410 all the names and dates and guilty parties and everything like that this is polite it's polite  
00:53:21.540 because of what it allows the reader to do it allows the reader to escape early what is your  
00:53:28.530 algorithm for reading the news given a webpage up with the news how do you read it or you scan it  
00:53:35.790 for a headline that you think is interesting okay if that headlines interesting I'll read the first  
00:53:40.620 paragraph you read the first paragraph if it's still interesting you read the second paragraph  
00:53:44.700 if it's still interesting you read the third paragraph if it's still interesting you read  
00:53:49.230 the fourth paragraph notice the while loop here and you exit that while loop as soon as you get  
00:53:55.470 bored that's how you read everything in the news nobody ever reads the whole article you just read  
00:54:01.050 until you're bored and then you get out this is polite it is polite to allow the reader to exit  
00:54:06.900 early and this allows you to exit early if you or some guy and you saw a call to that function  
00:54:13.740 render page we'll set up some paradigms and you said gee I wonder what that function does and  
00:54:20.340 then you went here within three seconds you know what this function does at a very high level not a  
00:54:27.900 level but maybe you didn't want to know it at a detailed level maybe you just wanted to know oh  
00:54:32.190 yeah okay if the test paid you include sit-ups and turnouts okay and you get out early there was no  
00:54:39.630 way to get out early from this code the only way to get out of this code at all is to understand  
00:54:53.100 it all completely including all the dots and it would have taken you thirty minutes thirty minutes  
00:54:59.700 you would have poring over this code wondering what the devil is this guy doing why is he doing  
00:55:03.990 it this way what's good and at the end of that thirty minutes you would have finally gone oh
00:55:07.770 if the test page he's including the setups and tear downs why the hell didn't he say that
00:55:24.900 how did I get this code shrunk down to that last little bit how did that process work my goal was  
00:55:40.530 to make these functions small and I call this the first rule of functions the first rule of  
00:55:46.380 functions first rule is that they should be small the second rule is they should be smaller than  
00:55:53.190 that I want to turn the knob up on this really high I want the function small really small how  
00:56:01.830 small should have function be let's see what's the proper size for a function how many lines  
00:56:07.530 should it be one for three sit on your screen that was the old rule by the way back in the  
00:56:21.540 back in the 80s when we first got screens does anybody remember when we first got screens but  
00:56:27.330 there was a time we didn't have screens we wrote our code on paper in the early days of programming  
00:56:33.000 programmers did not know how to type we had no keyboard skills we wrote the code in pencil and  
00:56:39.150 we had other people enter it into punch cards for us but then eventually we got screens and  
00:56:46.050 we started typing ourselves that was in the 80s and those screens a typical screen was 24 lines  
00:56:53.730 by 72 columns why 72 columns yes part holes on the punch card there were 80 columns on a punch  
00:57:03.600 card the last eight were sequence numbers so we only needed 72 on the screen that's the reason  
00:57:08.760 they were 72 you didn't even know they were 72 did you well now you know and the rule came about your  
00:57:17.070 function should fit on a screen well that meant that the functions had to be about 20 lines of  
00:57:21.390 code is that the right size I have a better rule a function should do one thing a function should do  
00:57:30.480 one thing that's the rule for how big a function should be it should do one thing but now we need  
00:57:36.750 to define what one thing is what's one thing what idea are you using i hear people going IntelliJ  
00:57:49.650 IntelliJ who's using IntelliJ oh yeah tell Jay it's the best idea it really is the best idea  
00:57:56.550 out there there's just nothing better is anybody using anything else if you are I'm sorry what is  
00:58:02.340 it eclipse and well VI you know everybody has to use VI from time to time cuz you've got to  
00:58:11.670 edit some text file and there's just no easier way to go bij jkkk it's fine right fine but if you're  
00:58:19.890 if you're editing code you don't want to use VI for that cuz that's a pain so you use something  
00:58:24.570 like IntelliJ that's nice okay IntelliJ it's got a refactoring menu one of the menu items in there is  
00:58:33.630 called extract method who's used extract method we can add everybody use extract method that's  
00:58:38.820 good okay so now you know how extract method works I'm going to define one thing a function does one  
00:58:47.340 thing if you cannot meaningfully extract another function from it if a function contains code and  
00:58:57.240 you can extract another function from it then very clearly that original function did more than one  
00:59:03.360 thing because you could extract something from it so I want all my functions to do one thing  
00:59:10.530 and that means I must extract and extract and extract and extract until I cannot extract any  
00:59:15.960 more I'm going to take all the functions in the system and explode them down into a tree of tiny  
00:59:22.200 little functions optimally extracted maximally extracted at this point there are people in the  
00:59:28.980 room going this guy's nuts he's insane I'm not going to do that if I did that I'd have thousands  
00:59:36.720 of little tiny functions I would drown in a sea of tiny little functions no you will not drown in a  
00:59:43.650 sea of tiny little functions and there's a simple reason why you're not going to drown in a sea of  
00:59:49.530 little functions and that's you're going to have to give those functions names you'll have to name  
00:59:56.310 those functions and as you name them you'll have to move them into appropriately named classes and  
01:00:02.160 appropriately named packages and appropriately named source files and modules and you will  
01:00:07.500 create a tree a semantic tree of functions that you can follow by name now you may not believe  
01:00:16.980 me but let me make a few points I'm going to draw for you a function that I wrote in 1988 probably  
01:00:34.380 the name of this function was gi gi stood for graphic interpreter it was 3,000 lines long I'm  
01:00:49.020 going to draw it for you not all the code just the shape of the code you recognize that don't  
01:00:58.830 you and that was the shape of the code well it was actually more complicated than that but
01:01:03.870 look at that shape and see if you can tell if there's some part of your brain that relaxes  
01:01:14.130 because you see that shape there's some part of your brain that could oh why why does that happen  
01:01:24.240 because beep when people get very used to a large function or a large module you've been working in  
01:01:30.540 it for months you know it you know it really well you know it geographically you know it by  
01:01:37.470 its landmarks you know the shape intimately and look at that shape rotate that shape 90 degrees  
01:01:45.930 it looks like the horizon humans evolved to know where they are on the planet by staring at the  
01:01:52.230 horizon they know over there by those Peaks that's where the watering hole is and the saber-tooth  
01:01:58.230 wanders over there so you look at that shape and some your brain goes yeah that looks like home  
01:02:06.810 now this was written in C by the way if someone had come to me in 1988 and said you know Bob  
01:02:18.600 this 3000 line function does a lot more than one thing I would have said no it can't it interprets  
01:02:23.850 graphics because the whole notion of one thing was so horribly subjective at the time but now I know  
01:02:31.590 how to make it objective extract extract extract until you cannot extract anymore now let's assume  
01:02:39.270 that this function written in C has some variables where were this where were the variables in C the  
01:02:49.140 local variables of a C function where did you put them at the top that's right C programmers  
01:02:54.180 in the room know right you still have your copies of Kern and Richie sitting on the Shelf don't you  
01:02:58.650 can't be far away from it so okay let's say that there's a couple of variables here int  
01:03:05.220 I and J good now let's say that this indent right here manipulates I and J and it does this it says  
01:03:23.460 I equals 0 and J equals 2 okay semicolon good now you want to practice extract till you drop do you  
01:03:34.830 want to do what I've just told you to do extract extract extract extract so you highlight that  
01:03:39.060 highlight that indent with your mouse and you invoke the extract method function of the IDE  
01:03:44.250 and the IDE will come back with an error message and say I can't extract it because it changes two  
01:03:50.310 variables and I can't extract code that changes two variables well what are you going to do now  
01:03:56.130 you want to extract it but you can't because it changes two local variables what are you gonna  
01:04:03.420 do now make them global right that works perfectly you think I'm joking don't you haha but now look  
01:04:24.220 you can extract that and you can extract this one into another function so now I've got two  
01:04:29.530 functions I've extracted them out I can take that one and extract it yeah I can take that  
01:04:33.370 one and extract it and I've got something very interesting here now I have a set of functions  
01:04:39.280 all of which manipulate a set of variables what's it called when you have a set of functions that  
01:04:45.670 manipulate a set of variables that's called the class there was a class hiding inside this  
01:04:51.580 big function and if you think about it of course there are classes that hide inside big functions  
01:04:57.310 because big functions have a whole bunch of variables and a whole bunch of indents that  
01:05:01.390 manipulate those variables so of course every large function is really a class with a bunch  
01:05:07.600 of little tiny functions inside it and if you start extracting and extracting and extracting  
01:05:12.760 you will begin to identify these classes that you would otherwise not have identified and you'll be  
01:05:18.100 able to put them in appropriate names and spaces and allocate them nicely and partition your code  
01:05:23.800 well this is what happens when you start to extract and extract and extract you find the  
01:05:30.490 true object-oriented structure of the system that you're trying to design we're all object-oriented  
01:05:36.190 designers we all use object-oriented languages what language are you using well you're doing  
01:05:40.060 intelligent must be doing Java right everybody doing Java who's doing Java Oh who invented Java  
01:05:48.190 and when and why his name was James Gosling in the year was 92 or 93 why did he invent this  
01:05:58.540 language Oh someone said it good good okay yes he was he worked at Sun Microsystems in the contract  
01:06:08.020 programming division and they got a a contract to write the code for a cable television set-top box  
01:06:15.040 now son was dedicated to C++ at the time so he was supposed to write this in C++ but he hated  
01:06:22.020 plus plus anybody here hate C++ okay fine well now you know why right so he hated C++ and decided he  
01:06:28.890 would write his own language which he called oke Oh a K oke and lovely fine he got it all to work  
01:06:37.350 and life was good and then the contract and didn't the language went in the garbage bin where it  
01:06:43.260 belonged that would have been the end of it except for an accident of history the accident of history  
01:06:50.640 is very interesting because Sun Microsystems was a hardware company they sold pieces of  
01:06:57.390 metal they sold chunks of hardware and their whole marketing scheme was about selling hardware and  
01:07:04.320 they realized in the early 90s that the best way to sell hardware was to win the hearts and minds  
01:07:11.250 of programmers first time that had been realized that programmers are the ones who make the buying  
01:07:19.710 decisions prior to that paper trying to sell the CIOs and CTOs and CEOs but when they realized  
01:07:26.220 that no no no it's actually programmers who make these buying decisions because they influence the  
01:07:30.540 executives never underestimate your power you're the ones who know and so son says how are we  
01:07:39.660 going to win the hearts and minds of programmers what better way than to give them the language of  
01:07:46.350 the Internet and so they looked around on what language should we use and here's Gosling over  
01:07:52.020 in the corner and he reaches into the garbage can and he takes this oak thing out and he says  
01:07:55.590 you could use this one I think executives in the market ears I'll say I have fine that's fine if  
01:08:02.760 I don't like the name it should have some better name something stimulating like coffee dalla and  
01:08:07.020 they won the hearts and minds of the programmers you'd like to think that Java was created out of  
01:08:16.439 some bursts of creative energy but it was foisted upon the programming community as a marketing tool  
01:08:24.810 to win your hearts and minds it was a manipulative event keep that in mind where did c-sharp come  
01:08:30.870 from Java it is Java Microsoft just took it does anybody remember visual j+ + right make yourself  
01:08:43.760 to him and sun microsystems said hey you can't do that and microsoft said oh ok we'll change  
01:08:48.890 the : ah how much indenting do you think you would do if you extract and extract and extract
01:09:09.410 how deep will your indenting be if you extract and extract an extract one or two yeah one or two is  
01:09:21.200 about it usually one well actually usually none right just open brace 1 indent couple of lines of  
01:09:28.640 code close your functions will really be about four lines long 3 4 or 5 lines long something  
01:09:33.770 like that every once in a while you'll get a 6 liner switch statements tend to get longer you  
01:09:38.930 don't like switch statements so you don't have too many of those I hope because they're evil  
01:09:43.279 switch statements bad things to have around really the same true with if-else statements  
01:09:46.910 we don't want those either so we tend to give these little tiny functions now think of what  
01:09:51.620 happens to normal code take an if statement if you are extracting and extracting and extracting  
01:10:01.100 what is the body of the if statement it's a function call yes and it has a nice name a  
01:10:10.550 name that tells you what the function is going to do so you say if something then do this it  
01:10:17.360 reads like well-written prose what's in the parenthesis of the if statement a function  
01:10:23.810 call with a nice name that tells you what you're testing if employee is too old fire employee it  
01:10:31.460 works perfectly right it reads like well-written prose how many arguments should a function have
01:10:46.850 well zeros a good number right cuz it's really easy to understand a function that takes zero  
01:10:52.730 arguments but you don't have to worry about any if statements in there that checking the  
01:10:56.660 arguments for anything there's no arguments one is not too hard to understand one argument going  
01:11:02.360 into a function is pretty easy that's you know mathematical f of X we kind of get that  
01:11:06.291 two arguments going into a function yeah it's okay you know the human brain is pretty good  
01:11:12.590 at keeping two things in order there's only two different ways to order them so not too  
01:11:17.541 hard three arguments it's a little hard how many ways are there to order three arguments six yes  
01:11:26.210 it's n factorial so ooh six is hard now humans can probably do that sort of what about four how many  
01:11:36.561 ways are there to order four arguments twenty-four twenty-four different orderings how about five one  
01:11:43.940 hundred and twenty ways is n factorial big crimes enormous ly fast so probably don't want any more  
01:11:49.880 than about three arguments in a function that's the way I like to limit it I don't like to have  
01:11:53.960 functions that take more than three arguments and by the way there's another there's another  
01:11:57.740 debate here I almost use the word argument um if you have a function and you want to pass six  
01:12:07.910 things into it those six things are so cohesive that they can be passed into a function together  
01:12:15.021 why aren't they already an object this is an interesting debate to have here you probably  
01:12:20.930 never need to pass more than three things into a function so I I like to use that as a a soft  
01:12:27.320 rule I don't want to see a long comma separated list of arguments that seems to me to be rude  
01:12:34.730 I would like it to be polite so keep the number of arguments down to two or three create objects  
01:12:43.041 if you have to use other strategies to to get things into functions by creating objects and  
01:12:50.180 data structures things like that what types of arguments should you never pass into a function
01:12:59.490 boolean's boolean's now by the way I use the word never never is the wrong word mostly never  
01:13:06.300 is probably a better way to put that don't pass boolean z' into functions why not well because  
01:13:12.540 if you pass a boolean into a function there must be an if statement in that function and that if  
01:13:18.210 statement has two branches the normal branch and the else branch why not just separate them  
01:13:22.500 into two functions call the one in the true case call the other in the false case have you ever  
01:13:27.960 read code that has boolean arguments it's rude now do this comma 5 comma 6 comma true what does the  
01:13:39.990 true mean I don't know it must be true though and here's how you read this code when you're reading  
01:13:48.600 this code you stare at that boolean and go oh [ __ ] wow that the author probably knew what he  
01:13:55.500 was talking about and you walk away you're gonna go read what the boolean does is probably some  
01:14:00.060 stupid if statement in the middle of the function don't pass boolean surround they're just annoying  
01:14:04.950 now that can't be a hard and fast rule because there are times when you want to pass a boolean  
01:14:09.840 around for example you are setting the state of a switch you know sets which bool ok fine but  
01:14:17.040 don't use it as a little testing argument into functions that's just rude it's annoying another  
01:14:24.300 thing that's rude is output arguments arguments that are passed into a function for the purpose  
01:14:32.370 of collecting the output nobody understands that right you're reading along and you've probably all  
01:14:38.850 had this experience where you're reading along and you read this line and there's an argument  
01:14:44.880 at the end of the function call and you're not quite sure why it's there it seems out out of  
01:14:49.830 context it's just a bizarre argument but you've got this vertical momentum as you're reading who's  
01:14:54.960 had this experience right you're reading down and there's something about this line that puzzles you  
01:15:00.120 but you've got this nice vertical momentum so you keep reading but a little process is started in  
01:15:05.760 your brain and this little process starts yelling at you louder and louder you didn't understand  
01:15:10.230 that last line you didn't understand that lining your eyes are torn back up to look at that line  
01:15:16.060 this process in your head takes your takes your head and moves it back to stare at that line this  
01:15:21.700 is a double take you know a double take it's I think it's American slang double take a double  
01:15:28.870 take is like this you're out on the road your start on the sidewalk you're walking down the  
01:15:33.160 street out of the corner of your eye you see an attractive individual you turn away and then a  
01:15:38.230 little process in your brain goes wait that was interesting and you go back that's a double take  
01:15:43.180 that code that makes you do a double take is rude it's rude code it forces you to stop your  
01:15:52.600 reading and go back so you don't want to have these double take moments in the code there's  
01:15:58.600 another author who says this is the principle of least surprise make sure that your code is  
01:16:05.860 not surprising do one thing yes I did that did that one yes yes that's all the abstraction at  
01:16:17.320 the same level yes extract to you drop did that good avoid switch statements why what's wrong  
01:16:26.230 with switch statements what's that they break why do they break oh but you might forget them  
01:16:39.010 yes okay good so switch statements what goes wrong with switch statements what do they do  
01:16:45.820 more than one thing that's true so let's say that we've got a switch statement the switch statement  
01:16:51.790 switches on oh the type of a shape so there's a shape and it's there's an enum somewhere that I  
01:17:03.580 define circle and square and triangle and so on traditional kind of kind of objects and you've  
01:17:09.520 got a switch statement that switches on this shape type and in fact how many such switch statements  
01:17:15.700 will there be in the system because your system deals with shapes how many such switch statements  
01:17:21.760 will there be well you're going to have to have a switch statement you draw a shape you'll have  
01:17:25.750 to have another one when you rotate a shape and another one when you erases shape and another one  
01:17:29.500 when you drag a shape another one when you stretch a shape in fact every time you do anything to a  
01:17:33.760 shape you'll have to have a switch statement now what goes wrong you add a new type of shape what  
01:17:42.880 happens when you add any type of shape you've got to find all the switch statements you've got  
01:17:47.620 to go through the whole code you've got to find all the switch statements are you going to find  
01:17:52.540 them all are they all switch statements they might some of them might be if-else statements  
01:17:59.350 and then there's this problem rotate shape what do you do in the circle case nothing there's no  
01:18:11.020 case for the circle so the programmer does some nice logical optimizations here's the  
01:18:16.479 problem now right you've got to add a new type of shape rhombus and you've got to find every  
01:18:23.560 switch statement in the system and you've got to investigate every switch statement in the system  
01:18:28.660 logically decoding them to make sure you put the rhombus part in in just the right place and  
01:18:35.050 this is fragile this breaks it's hard to do it causes lots of difficulties what's the solution
01:18:43.000 polymorphism is the solution of course we don't want to have a switch statement we want to have  
01:18:51.130 a base class called shape and we want to have subclasses for triangle and circle and square  
01:18:56.740 and rectangle and all of these interesting derivatives and then we can put all of those  
01:19:01.479 functions into the derivatives we can put the draw function and the rotate function and the  
01:19:06.610 drag function and the square file all of those functions can go into the derivatives and now  
01:19:11.709 what happens when we add a new shape what changes in the system when we add a new shape we have to  
01:19:24.790 add a new file new class new subclass yes but nothing else in the system changes there are  
01:19:33.459 no such statements the switch statements are all gone nothing else in the system changes this is  
01:19:42.370 called the open closed principle the open closed principle says that a system a module should be  
01:19:47.440 open for extension but closed for modification you should be able to extend the behavior of a  
01:19:52.360 module without modifying that module and how do you do that well you do that by creating  
01:19:57.910 base classes and having derivatives our system can now be extended with new shapes without modifying  
01:20:06.610 anything in the system we have to add something to the system we don't have to modify anything  
01:20:10.900 this is one of the reasons we don't like switch statements but just one there are other reasons  
01:20:17.110 we don't like switch statements we don't like switch statements and I better draw this one
01:20:22.780 doo-doo-doo-doo here's a switch statement with a bunch of cases it's in a module each of these  
01:20:43.750 cases does something but let's say in this particular case these cases call out to other  
01:20:53.110 modules look at the dependency structure here we've got a single module that throws a whole  
01:21:05.380 bunch of dependencies out to other modules these are source code dependencies the switch statement  
01:21:09.820 must import all of these called modules so the import statement is a long import statement  
01:21:17.110 there's a source code dependency going from the switch statement to every one of these outgoing  
01:21:21.220 modules now we've got other modules in the system that depend on the switch statement  
01:21:30.220 if I make a change there what has to recompile everything everything has to recompile what has  
01:21:43.960 to redeploy everything everything to the left the switch statement has to be redeployed and  
01:21:51.940 everything to the left of the switch statement has to be redeployed because it's all getting a  
01:21:54.940 recompiled switch statements act as a dependency manager manic magnet a dependency magnet they  
01:22:04.960 throw out a big net of dependencies that cause it to be difficult to independently deploy modules  
01:22:12.010 do you independently deploy do you do you a move your system into jar files do you partition your  
01:22:21.070 system into jar files what is a jar file yeah it says Java archive that's what jar stands for fine  
01:22:31.030 thank you but what is the intent of a jar file what is the equivalent in dotnet a DLL what this  
01:22:44.080 DLL stands for dynamically linked library what does it mean to be dynamically linked first of  
01:22:51.310 all what does it mean to be linked see programmers know what does it mean to be dynamically linked
01:22:57.760 everything gets loaded at runtime all the external variables get linked at runtime that's what a  
01:23:08.380 jar file is it's a runtime linking loader does anybody remember the 80s when we would execute  
01:23:18.430 absolute binaries who was the C programmer in the 80s oh yeah some guys here C programmers in  
01:23:27.700 the 80s right again how did you do this right you would compile dot C files into dot o files that  
01:23:33.100 were object files and you collect all your files and then you'd link all without o files together  
01:23:37.570 into a dot XE and then you could execute the XE and by the way the longest-running effort there  
01:23:44.050 was the link the link took forever and why did the link take forever because disks were slow  
01:23:49.870 and we didn't have a lot of memory in those days but as we got into the 90s that really  
01:23:55.510 got faster the disks got smaller and faster and really tiny and much more reliable and link time  
01:24:02.080 started to shrink like crazy and Microsoft was one of the first ones to say hey we could use that to  
01:24:07.210 dynamically link so they created ActiveX anybody remember ActiveX horrible decom stuff okay and  
01:24:15.490 then along comes Java and we've got jar files and we've got DLLs that life is good so a jar  
01:24:21.370 file is there so that you can independently deploy chunks of your system now what would  
01:24:30.250 you like to independently deploy so you don't have to deploy everything what would you like  
01:24:37.930 to independently deploy why what would you like to deploy separately well now let's think about  
01:24:47.680 this yes modules is the right answer but which modules what modules would you like to deploy  
01:24:53.440 separately from which other modules which parts of the system changed capriciously change for  
01:25:00.370 stupid reasons the business requirements change for stupid well they're not stupid reasons right  
01:25:06.520 they change but it's not for stupid reasons the stuff that's changes for stupid reasons  
01:25:13.180 is the GUI there are people that just say you know I don't like the color that color should  
01:25:18.940 be a different color I don't like to say I don't like the shape of that thing that  
01:25:21.430 should be around and let's take that page and move stuff around on it has nothing to do with  
01:25:24.820 the business rules at all has something to do with some marketing guys idea of what would  
01:25:28.810 look better on the screen how many of you have faced the problem that when there's a change to  
01:25:34.030 the GUI it breaks a whole bunch of business rules you don't want that to happen you want to isolate  
01:25:40.330 the GUI from the business rules we're going to talk about that tomorrow and we talked about  
01:25:44.260 clean architecture and so what we'd like to be able to do of course is independently deploy  
01:25:50.350 the GUI from the business rules independently deploy the database from the business rules so  
01:25:56.470 that if there's a change made to the GUI we can redeploy the GUI without redeploying the rest  
01:26:00.820 of the system and we can do that if we are very careful not to throw switch statements all over  
01:26:07.630 inside the systems that was why we don't like switch statements there's a letter from my wife
01:26:28.190 okay I'm gonna read this there is no water in the building because a  
01:26:41.060 pipe broke in the street this problem might be resolved before 11 o'clock
01:26:46.550 how are your bladders you can continue until 11:00 I see we're not going to stop at 10:45  
01:26:59.510 I get it they're trying to tell me this so I actually didn't have to read this to you if  
01:27:05.000 there is still no water at 11 o'clock we'll be doing an announcement before that so okay fine  
01:27:09.920 all right we understand thank you thank you got it all right 11 o'clock now all right I'm going  
01:27:24.320 to talk about names a little bit later I've talked about arguments already good and flag arguments  
01:27:35.690 and output arguments no side effects what's a side effect so the classical definition of a side  
01:27:46.220 effect is a change to the state of the system if you call a function and that function causes the  
01:27:55.460 system to change state then that function had a side effect the function open has a side effect  
01:28:03.950 because it leaves a file open the function new has a side effect because it leaves a block of  
01:28:13.970 memory allocated side effect functions change the state of the system side effect functions come in  
01:28:24.110 pairs there's open and closed new and free are new and delete in C++ and Java we fixed that problem  
01:28:33.830 semaphores season release side effect functions come in pairs they're like the SIF always to there
01:28:41.990 now how good at we at managing pairs of functions like this for example how good are we at managing  
01:28:53.780 Alec and free the answer to that is that we're terrible at managing that and the the obvious  
01:29:07.041 evidence of our terrible ability to manage pairs of functions is that in modern languages we've  
01:29:13.791 invented a horrible hack to allow us to forget about managing pairs of functions that horrible  
01:29:21.140 hack is called garbage collection now I would not want to program without garbage collection  
01:29:25.521 because garbage collection makes it much easier to write safe code but you must admit that garbage  
01:29:35.780 collection is a crutch it is not reasoned you did not write the code in a reasoned way you  
01:29:43.371 did not free everything you allocated instead what you did is said the system will take care  
01:29:48.980 of it and ok fine we have we have written this horrible hack we've we've declared that we're  
01:29:56.900 going to depend upon it we've acknowledged that we need the crutch but allocate allocation and  
01:30:03.291 freeing is not the only side effect function there are many other side effect functions  
01:30:08.601 that you and I have to deal with like open and close does anybody seen a system crash because  
01:30:13.610 too many people forgot to close files yes and you leave a bunch of file descriptors open in  
01:30:19.880 the operating system and eventually you run out of file descriptors that's called a file descriptor  
01:30:23.630 leak has anybody seen a system crash because all the graphics contexts got leaked or the semaphores  
01:30:30.081 didn't get closed anything that comes in a pair like that will suffer the same fate that memory  
01:30:37.280 used to suffer when we had memory leaks and by the way you can still have memory leaks in Java  
01:30:42.500 you just have to have to work really hard at it so what do we do about this what do we do  
01:30:49.610 about this problem of of controlling pairs of functions pairs of functions must be called in  
01:30:59.300 the right order you cannot close a file before you open it you cannot free memory before you  
01:31:06.410 allocate it if you call them in the wrong order it's a logical inconsistency how many of you have  
01:31:12.050 debug based system spent days and days debugging a system only to find that you can make it work  
01:31:18.590 if you change the order of two function calls and you don't know why but somewhere in there  
01:31:27.200 there's some side-effect and if you just change the order oh that makes everything work what can  
01:31:37.220 we do to manage that so up what version of Java are you using you're working on lambdas now Java  
01:31:47.270 but what is that eight Java eight everybody doing Java eight everybody familiar with lambdas now you  
01:31:51.350 know how to do lambdas and you can pass lambdas here and there and everywhere good I don't know  
01:31:58.910 why they put that feature in the language so what can you do about this well if you've got lambdas  
01:32:07.760 in the language it makes things a lot easier well it makes things a little bit easier remember that  
01:32:12.770 the lambda is just a class and it's a class with one function in it called execute you could have  
01:32:17.510 written that yourself but they decided to put it in the language so okay fine let's say that  
01:32:22.250 we want to make open safe and I think in the Java library now they actually do this so okay here's  
01:32:30.230 our open function our open function is going to be a void it doesn't return anything and it's  
01:32:38.600 going to take a filename so that's a string that would be the file that we're going to open and  
01:32:45.170 then we're going to pass into it a lambda and this will be we'll call this lambda process of course  
01:32:53.840 I'm not using Java syntax but you get the point okay there's the open brace now what do we do  
01:32:58.880 well first thing we're going to want to do is we're going to really open that file so we're  
01:33:03.740 going to call the low-level open function so here we say file F equals file dot open of FN good so  
01:33:16.790 now we've got the open file the next thing we do is we say process that file so now we're calling  
01:33:23.330 the lambda and now we say file dot close 1/2 and return this is a simple function that deals with  
01:33:41.660 the side effect this function does not have a side effect because it leaves the system with the file  
01:33:47.570 closed so the side effect is dealt with inside of this function you don't have to remember to close  
01:33:54.290 it what you do have to remember to do is pass in the lambda that processes the open file this  
01:34:00.020 is a very common procedure for dealing with side effects so you can try to get your side effects  
01:34:05.990 under as much control as possible by passing a lambda into your system if you don't have lambdas  
01:34:12.740 in your language then you could use a command object a simple a simple class that has one  
01:34:17.900 function in it called execute and then you pass in the appropriate derivative data side effects  
01:34:29.300 doop-doop do yep good command query separation a function that returns void must have a side-effect  
01:34:42.350 if it doesn't have a side-effect there's no point in calling it so a function that returns a value  
01:34:50.720 should not have a side-effect this is a convention that we like to follow called command and query  
01:34:56.660 separation commands change the state of the system therefore they return void anything that returns  
01:35:04.160 of value by convention will not change the state of the system and that way when you see a function  
01:35:11.180 that returns a value you know it's safe to call it it will leave the system in the same state it  
01:35:16.400 was found in this is a convention that I like to follow the language doesn't enforce it of course  
01:35:22.100 but I like to follow it because it allows me to keep track of side-effects to prefer exceptions  
01:35:32.270 to returning error codes do I even need to go over this dude use since exceptions a lot  
01:35:39.260 in Java everybody using exceptions good good good does anybody remember how awful exceptions were in  
01:35:46.580 C++ okay so we didn't use them I don't use them in C++ and Java they're fairly safe it is better to  
01:35:53.810 use an exception than to return an error code I'm not going to belabor that point but I will make  
01:35:59.840 another point when I write a try block the only thing in that function that has the try block is  
01:36:09.920 the try block I don't want to have a lot of code before the try block if I've got a function that  
01:36:16.700 throws an exception the first executable statement in that function is going to be try and then there  
01:36:22.820 will be a single function in the try block that's the function that actually throws the exception  
01:36:28.520 and then there will be a closed brace and then the catch blocks and a finally block if necessary and  
01:36:33.560 then no other code in the function I don't want anything in the function except the try block I  
01:36:39.260 don't want a whole bunch of code in the try block I want that to be a different function I don't  
01:36:43.340 want any prefix code in that function I don't want any suffix code in that function just the try  
01:36:48.500 block because error processing is one thing so I want the I want the try block completely contained  
01:36:54.350 by a function and I never ever ever want nested track patch blocks I will find you if you do that
01:37:00.860 there's a rule in software called the DRI principle don't repeat yourself this has to  
01:37:17.750 do with duplicate code you saw some duplicate code in that original code that I threw up on  
01:37:22.010 the screen a little bit earlier that code was obviously copy and pasted we'd like to avoid  
01:37:29.210 duplication as much as possible because it's sloppy if you do you copy and paste a bunch  
01:37:34.640 of code it's just sloppy to leave it in that state what you'd like to do is move the copied  
01:37:40.670 code into some function and call of a function of course sometimes you copy the code and then  
01:37:46.130 change the code you've copied and so that means you're probably going to have to put it into a  
01:37:51.020 function that has some arguments but that's all fairly easy to do so we don't like duplicated  
01:37:57.560 code we don't like code that's kind of duplicated we ought to be able to move those into functions  
01:38:03.920 but what do you do when it's not the code that's duplicated it is the loops that are duplicated how  
01:38:13.880 many of you have seen this problem where you've got a complex configuration data structure to  
01:38:19.430 walk this complex configuration data structure you have to have a big nested bunch of loops  
01:38:23.810 a bunch of while loops and if statements that allow you to walk through the Kombi the complex  
01:38:29.900 configuration data structure and then you finally get to the end nodes in there and you've got a  
01:38:35.000 bunch of processing code that processes the end nodes and then you see that same loop repeated  
01:38:40.460 over and over and over again inside different parts of the system as the different parts of the  
01:38:44.510 system walk different parts of the configuration database how can you get rid of that duplication  
01:38:51.380 one of the answers to that as well if you've got lambdas you can put that nice looping structure  
01:39:00.590 into a function that takes a lambda argument and then you pass the processing code into the  
01:39:05.480 lambda argument so you can get all of those duplicated loops down into one and then just  
01:39:11.900 pass a lambda in or pass an object that takes a a single single parameter which is a function
01:39:25.090 we got about 10 minutes I'm gonna lighten them lighten the mood a little bit structured  
01:39:34.330 programming what is it what is this thing called structured programming when was it  
01:39:40.180 invented who invented it why Michael Jackson no actually although he did a lot of a lot of  
01:39:47.440 books on structured programming it was invented by Edsger Dijkstra here in the Netherlands Edgar  
01:39:53.680 Dijkstra was the the first programmer of in the Netherlands one of the first programmers in the  
01:39:59.050 world but the very first programmer in the Netherlands he survived the Nazi occupation  
01:40:04.150 of during World War two he came out of that and and went into school he wanted to become  
01:40:09.730 a nuclear physicist he saw computer the very first computer in the Netherlands and fell in  
01:40:16.390 love with it he wanted to study software but he went to his his advisor and said I there's no  
01:40:23.650 body of knowledge here there are no disciplines there's no formalism I don't think my peers will  
01:40:28.390 take me seriously if I study computers and his advisor said well Metzger maybe you will be one  
01:40:35.200 of the people who will invent those formalisms and disciplines and add to the body of knowledge  
01:40:40.180 and Edsger Dijkstra took that as a challenge and became the Netherlands first programmer he went on  
01:40:48.790 to study software in those very early days in the 1950s interesting sidenote he got married  
01:40:57.400 I don't remember the date but it was in the late 40s or early 50s that he got married and he had to  
01:41:03.400 put his occupation on his marriage license and he put programmer and the officials wouldn't accept  
01:41:10.120 it because that wasn't a known profession so he changed it to nuclear physicist he said he made  
01:41:20.410 the more difficult choice programming was more difficult than nuclear physics in 1968 Dijkstra  
01:41:33.010 wrote a letter to the editors of one of the famous magazines at the time and he said that go-to was  
01:41:40.290 considered harmful very famous note and it it kind of caused an uproar in the tendons in the  
01:41:49.050 software community because it during those days go to was how we got things done we used to go  
01:41:55.920 to for everything we had if statements that would have go to Xin them we didn't have languages that  
01:42:01.890 had while loops in them we would just use go twos all over the place anybody here remember  
01:42:06.750 Fortran anybody for to add weights before trying programmers in the room yeah since go two's for  
01:42:12.750 everything and so Dykstra comes along and says yeah go to Zura problem and the world kind of  
01:42:17.640 went nuts for about five years now we didn't have a face book in those days you couldn't  
01:42:23.130 flame anybody right so what you did instead is you wrote letters to the editor of the trade journals  
01:42:29.310 and people wrote the most scathing letters to the editor Dijkstra's an idiot because of this  
01:42:33.900 and other people would say no Dijkstra's a god because of this and these letters went back and  
01:42:38.460 forth and it all settled out do you have a go to in your language no you don't do you and no  
01:42:49.410 modern language has a go to some modern languages reserve the word did you know in Java that go-to  
01:42:55.740 is a reserved word specifically so that it cannot be implemented right Dijkstra won that battle he  
01:43:04.740 won it by all the languages beginning to adopt the the structure now why did Dijkstra not want  
01:43:13.680 DotA's what what was harmful about go twos so Dicer was an interesting fella he wanted to turn  
01:43:21.780 software into mathematics he wanted to create a set of postulates and then a set of theorems  
01:43:29.490 like Euclidian geometry he wanted to construct theorems of software his his vision was that you  
01:43:38.400 and I would write applications by adopting well proven theorems and then writing little lemmas  
01:43:46.320 to adapt them that was his idea there would be this vast library of fear proven software and  
01:43:53.610 so he began to work through the mechanics of how you prove software correct and and the mechanics  
01:44:03.120 were fascinating what he discovered was that you could write a simple proof for any two sequential  
01:44:09.120 lines you could write a slightly more complicated proof for an if statement to prove a loop correct  
01:44:17.940 you had to use induction so that was a little more complicated but bit by bit he was able to  
01:44:24.030 build up a simple structure of mathematical proof but then he noticed something there were certain  
01:44:32.370 algorithms that could not be proven correct there was no rational way to prove them correct and he  
01:44:38.910 discovered that those algorithms were were those that had unrestrained go twos this is something  
01:44:46.470 that actually touring had proved in 1936 it's the halting problem it goes back to the halting  
01:44:51.900 problem of 1936 which is that there are certain algorithms that cannot be proven in that cannot  
01:44:57.960 be proven correct the reason they cannot be proven correct is because they have unrestrained go twos  
01:45:02.220 he fell back on another theorem that two other guys had written about the same time which was  
01:45:08.220 that every algorithm can be composed of three structures sequence selection if-then-else  
01:45:15.660 and and iteration loops every algorithm can be written out of those three structures so  
01:45:21.870 Dijkstra said okay no go deuce everybody has to use these three structures that will guarantee  
01:45:27.240 that the algorithms can be proven correct we never bothered to prove anything correct  
01:45:36.060 Dijkstra's whole vision failed we don't prove our software correct we fell back on something else
01:45:42.060 Dijkstra's vision was this mathematical superstructure like Euclidian geometry we  
01:45:49.860 do not have that Dijkstra's vision failed but we do have something else there is a branch of  
01:45:57.750 knowledge that you and I risk our lives on every day our lives depend on this branch of knowledge  
01:46:03.360 every day it cannot be proved correct and yet we very happily risk our lives with it every  
01:46:10.020 day this branch of knowledge is called science science is a set of conjectures and hypotheses  
01:46:17.130 that cannot be proven correct experiments can prove them false but never true, you and I risk  
01:46:25.500 our lives every day on things that are not proven correct we have not proven that airplanes can fly  
01:46:30.600 right the science is not proven correct it has simply been surrounded with so many tests that  
01:46:37.560 there's no point in saying that it's not correct any more software could be proven correct but we  
01:46:45.870 abandon that we gave up on it it's too hard but we can test it we can use science and  
01:46:54.180 we can write tests that demonstrate that the software is not failing we don't know if it's  
01:47:03.360 correct but it's not failing we treat software like a science not like a mathematics this was  
01:47:10.860 Dykstra's failure Dijkstra thought we were headed headed towards the mathematics or not  
01:47:15.240 we're headed towards of science and we prove our software not incorrect I'm sorry for the double  
01:47:23.220 negative that's the only way to say that we don't prove our software correct we demonstrate that it  
01:47:27.720 is not incorrect by surrounding it with tests how many of you write tests ok that's not all of you
01:47:39.160 so then I have to ask what the hell is the matter with the rest of you okay so  
01:47:43.930 how many of you write tests for every line of code that you write mmm that's a much smaller  
01:47:54.610 number isn't it why would that be why would you not test every line you wrote what would  
01:48:01.090 motivate you to bypass testing lines that you wrote and I'm gonna write these lines I'm not  
01:48:07.210 gonna test them why would you do that if they answer that question is part of the  
01:48:13.480 ethics issue here now right why would you write code and then not test it now you might say ok  
01:48:21.670 well I'm testing the whole system yes but are you testing the individual lines that you wrote
01:48:29.350 we'll talk about that later but I think it's 11 o'clock and it's the water working
