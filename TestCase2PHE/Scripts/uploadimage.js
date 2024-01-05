function encodeFile(input) {
    const file = input.files[0];
    const reader = new FileReader();

    reader.onloadend = function () {
        const base64String = reader.result.split(",")[1];
        input.previousElementSibling.value = base64String;
        console.log(base64String)
    };

    if (file) {
        reader.readAsDataURL(file);
    }
}
