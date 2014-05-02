// Persistence Of Vision raytracer version 3.0 file
//
// Church of Virus ideohazard logo
// (c) 1997 David McFadzean (david@lucifer.com)
//
// Dimensions based on a postcript file by Jed Hartman

#include "colors.inc"
#include "shapes.inc"
#include "textures.inc"
#include "stones.inc"
#include "glass.inc"
#include "woods.inc"
#include "ash.map"

#version 3.1;

#declare foocos         = cos(radians(clock*360))

#if (clock < 0.5)
#declare rot            = 180 * (0.5 + cos(radians(clock*360 + 180))/2)
#else
#declare rot            = 180 + 180 * (0.5 + cos(radians(clock*360))/2)
#end

#declare outcent        = 100
//#declare incent       = outcent * -11/8
#declare incent         = outcent * foocos * 11/8
#declare inrad          = outcent * 9/10
#declare outrad         = outcent * 5/4

#declare ringrad        = outcent * 9/10
#declare ringwidth      = outcent/8
#declare ringcutwidth   = outcent/25
#declare centcutwidth   = outcent/10
#declare centrad        = outcent * 3/8

#declare bwidth         = outcent/20

#declare Moon =
intersection {
	sphere { <0, outcent, 0>, outrad }
	plane {  z, bwidth }
	plane { -z, bwidth }
}

#declare CutMoon =
union {
	sphere { <0, incent, +86.7468>, outrad }
	sphere { <0, incent, -86.7468>, outrad }
}

#declare Moons =
difference {
	union {
		object { Moon }
		object { Moon rotate <0, 0, 120> }
		object { Moon rotate <0, 0, 240> }
	}

	object {CutMoon }
	object {CutMoon rotate <0, 0, 120> }
	object {CutMoon rotate <0, 0, 240> }
}

#declare Ring =
intersection {
	sphere { <0, 0, 0>, ringrad + ringwidth }
	sphere { <0, 0, 0>, ringrad - ringwidth inverse}
	cylinder { <0, incent, -2*bwidth>, <0, incent, 2*bwidth>, inrad - 10 }
	plane {  z, bwidth }
	plane { -z, bwidth }
}

#declare CutRing =
difference {
	cylinder { <0, incent, -2*bwidth>, <0, incent, 2*bwidth>, inrad }
	cylinder { <0, incent, -2*bwidth>, <0, incent, 2*bwidth>, inrad - 10 }
}

#declare Cut =
intersection {
   plane {-x, centcutwidth/2 }
   plane { x, centcutwidth/2 }
   plane {-y, -(centrad-1) }
   plane { y, abs(incent)-inrad+1 }
   plane {-z, 2*bwidth }
   plane { z, 2*bwidth }
   rotate rot*z
}

#declare Centre =
cylinder { <0, 0, -2*bwidth>, <0, 0, 2*bwidth>, centrad }

#declare Biohazard =
difference {
	union {
	   object { Moons }
	   object { Ring }
       object { Ring rotate <0, 0, 120> }
       object { Ring rotate <0, 0, 240> }
	}

	object { Cut }
	object { Cut rotate <0, 0, 120> }
	object { Cut rotate <0, 0, 240> }

	object { Centre }

	bounded_by { sphere { <0, 0, 0>, outrad+outcent } }
}

camera {
   location  <0, 100,-1000>
   direction <0,   0,    2>
   up        <0,   1,    0>
   right     <4/3, 0,    0>
   look_at   <0,   0,    0>
   //rotate rot*y
}


light_source {<300, 300, -1000> color White}

#declare TheSky=
sky_sphere {
    pigment {
          gradient y
          color_map {
            [0.000 0.002 color rgb <1.0, 0.2, 0.0>
                         color rgb <1.0, 0.2, 0.0>]
            [0.002 0.200 color rgb <0.8, 0.1, 0.0>
                         color rgb <0.2, 0.2, 0.3>]
          }
          scale 2
          translate -1
        }
        pigment {
          bozo
          turbulence 0.65
          octaves 6
          omega 0.7
          lambda 2
          color_map {
              [0.0 0.1 color rgb <0.85, 0.85, 0.85>
                       color rgb <0.75, 0.75, 0.75>]
              [0.1 0.5 color rgb <0.75, 0.75, 0.75>
                       color rgbt <1, 1, 1, 1>]
              [0.5 1.0 color rgbt <1, 1, 1, 1>
                       color rgbt <1, 1, 1, 1>]
          }
          scale <0.2, 0.5, 0.2>
        }
        rotate -135*x
}

plane { z, 200 pigment {Black} }
//plane { z, 100 pigment {Gray} finish {ambient 1} }

object {
	Biohazard
	pigment {Gray} finish {ambient 1}
	//texture { T_Stone18 scale 50 }
    rotate rot*z
    //rotate 180*z
    //rotate rot*y
}

#declare TimeStamp=
text {
     ttf "timrom.ttf" str(rot,5,2)
     1, 0
     pigment { Red }
     translate <-2.25, 1, -1>
     scale 100
}

