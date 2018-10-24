# c-sharp-music-playlist

# Develop a music playlist library

- Basic Requirements
	Maintain a list of songs. For each song - its name, artist, genre and identifier.

	Should be able to:
	1. Add a song. 
	2. If a song with the specified name, artist and genre exists, indicate an error.
	3. Return a list of songs by artist.
	4. Return a list of songs by genre.
	5. Return a list of all songs.
	6. Update a song.

- Detailed Requirements
	
	1. Adding songs.
	1.1. Should happen with a request object, separate from the song's domain.
	1.1.1. Should contain information about the song's artists and genres.
	1.2. Adding the song creates a new Song domain object and generates an unique string id.
	1.2.1. Adding a song that matches the exact artists and genres of another song throws SongAlreadyExistsException.
	1.3. The song is saved in-memory.

	2. Retrieving songs.
	2.1. Songs can be retrieved by their unique string id.
	2.1.1. Retrieving a song that exists returns the nullable Song object.
	2.1.2. If the song is not found a null is returned.
	2.2. Songs can be retrieved in a group by genre.
	2.2.1. If songs match the specified genre, they are returned in a list.
	2.2.2. If no song matches the given genre then an empty list is returned.
	2.3. Songs can be retrieved by artist.
	2.3.1. If songs match the specified artist, they are returned in a list.
	2.3.2. If no songs match the specified artist, an empty list is returned.

	3. Updating/Upserting songs.
	3.1. Songs can be updated by passing a Song object as an argument.
	3.1.1. Updating a song replaces it in memory.
	3.1.2. If an update request contains a Song object that does not match any other in memory
	then the song is inserted instead.