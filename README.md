# c-sharp-music-playlist

# Develop a music playlist library

- Requirements
	Maintain a list of songs. For each song - its name, artist, genre and identifier.

	Should be able to:
	1. Add a song. 
	2. If a song with the specified name, artist and genre exists, indicate an error.
	3. Return a list of songs by artist.
	4. Return a list of songs by genre.
	5. Return a list of all songs.
	6. Update a song.

- Extended requirements
	- Retrieving songs
		1. When retrieving with an non-existent or incorrect id, should return null.
	- Updating songs
		1. When updating a non-existent song, that song should be inserted instead.