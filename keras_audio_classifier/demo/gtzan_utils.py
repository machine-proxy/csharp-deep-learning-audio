gtzan_labels = {0: 'blues', 1: 'classical', 2: 'country', 3: 'disco', 4: 'hiphop', 5: 'jazz', 6: 'metal',
                7: 'pop', 8: 'reggae', 9: 'rock'}
				
import subprocess
import os

def convert_gtzan_to_mp3(gtzan_folder_path, output_folder_name):
    # recursively walk through the directory
    for dirpath, dirnames, filenames in os.walk(gtzan_folder_path):
        for filename in filenames:
            input_filepath = os.path.join(dirpath, filename)
            output_folderpath = dirpath.replace(gtzan_folder_path, output_folder_name)

            # create the folder if the folder does not exist
            if not os.path.exists(output_folderpath):
                print('creating folder : {}'.format(output_folderpath))
                os.makedirs(output_folderpath)

            output_filename = os.path.join(output_folderpath, filename[:-2] + 'mp3')
            print('converting : {}'.format(filename))

            # use ffmpeg to convert file to mp3 formats
            # -y overrites any existing files
            # -i specifies input file
            # -codec:a denotes that data is audio, and specifies the code to use
            # -b:a 128k = audio bitrate 128k
            completed_process = subprocess.run(
                    'ffmpeg -y -i {} -codec:a libmp3lame -b:a 128k {}'
                    .format(input_filepath, output_filename),
                    shell=True, check=True)

            # checks that it has been converted properly
            completed_process.check_returncode()

if __name__ == '__main__':
    # this script must be placed with gtzan_au folder
    convert_gtzan_to_mp3('./gtzan_au', 'gtzan_mp3')
