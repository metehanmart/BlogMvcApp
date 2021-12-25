function sifreGosteren() {
    var x = document.getElementById("sifreKutusu");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}