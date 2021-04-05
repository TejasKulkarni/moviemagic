export const splitStringIntoArray = (stringValue, separator = ",") => {
    if (stringValue != null) {
        return stringValue.split(separator);
    }

    return [];
}