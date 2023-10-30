FILE_EXTENSION_ERROR = 1
FILE_NOT_FOUND_ERROR = 2
EMPTY_FILE_ERROR = 3
REGION_ERROR = 4
NO_REGION_ERROR = 5
COLUMN_ERROR = 6
COLUMN_VALUE_ERROR = 7


def output_file_extension_error():
    print('ERROR: only .csv files are allowed!')


def output_file_not_found_error():
    print('ERROR: file not found!')


def output_file_size_error():
    print('ERROR: file is empty!')


def output_no_regions_error():
    print('ERROR: no regions in file!')


def output_region_error():
    print('ERROR: there is no such region in table!')


def output_column_id_error():
    print('ERROR: wrong column! Column must be integer between 1 and number of columns!')


def output_column_value_error():
    print('ERROR: cell value is not number!')


def show_error_message(code):
    if code == FILE_EXTENSION_ERROR:
        output_file_extension_error()
    elif code == FILE_NOT_FOUND_ERROR:
        output_file_not_found_error()
    elif code == EMPTY_FILE_ERROR:
        output_file_size_error()
    elif code == NO_REGION_ERROR:
        output_no_regions_error()
    elif code == REGION_ERROR:
        output_region_error()
    elif code == COLUMN_ERROR:
        output_column_id_error()
    elif code == COLUMN_VALUE_ERROR:
        output_column_value_error()
