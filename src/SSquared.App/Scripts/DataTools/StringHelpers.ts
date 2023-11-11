export function isNullOrEmpty(val: string | null | undefined) {
    if (!val || val == "") {
        return true;
    }
    else {
        return false;
    }
}

export function isNullOrWhiteSpace(val: string | null | undefined) {
    var safeVal = val ?? "";
    var trimmedVal = safeVal.trim();

    return isNullOrEmpty(trimmedVal);
}