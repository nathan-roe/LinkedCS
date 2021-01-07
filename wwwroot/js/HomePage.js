$(document).ready(() => {
// ______________ Save information to Session ________________ //
    var bioInput = "";
    var photoInputs = ["", ""];
    var photoName = "Default";
    var backName = "Default";
    $(document)
        .on("change", "#bioTextArea", e => {
            bioInput = e.target.value;
            $("#bioTextArea").val(bioInput)
        })
        .on("click", "#next", () => {
            if(count == 0)
            {
                document.getElementById('pFile') ?
                    photoInputs[0] = document.getElementById('pFile').textContent :
                    photoInputs[0] = "Default";
                    document.getElementById('bFile') ?
                    photoInputs[1] = document.getElementById('bFile').textContent :
                    photoInputs[1] = "Default";
                $.ajax({
                    url: '/updatePopup',
                    method: 'post',
                    data: {photos: photoInputs, count: 0}
                });
            }
            if(count == 1)
            {
                $.ajax({
                    url: '/updatePopup',
                    method: 'post',
                    data: {bio : bioInput, count: 1}
                });
                bioInput = "";
            }
        })
        .on("keydown", e => {
            if(e.keyCode == 13 && count == 0)
            {
                document.getElementById('pFile') ?
                    photoInputs[0] = document.getElementById('pFile').textContent :
                    photoInputs[0] = "Default";
                
                document.getElementById('bFile') ?
                    photoInputs[1] = document.getElementById('bFile').textContent :
                    photoInputs[1] = "Default";
                
                $.ajax({
                    url: '/updatePopup',
                    method: 'post',
                    data: {photos: photoInputs, count: 0}
                });
            }
            if(e.keyCode == 13 && count == 1)
            {
                $.ajax({
                    url: '/updatePopup',
                    method: 'post',
                    data: {bio : bioInput, count: 1}
                });
                bioInput = "";
            }
        });
// __________ Change Popup Window View ___________ //
    var count = 0;
    $(document).on("keydown", e => {
        if(e.keyCode == 13 && count < 2){
            e.preventDefault();
            if(count == 0)
            {
                $(".popup").load("changeToBio");

                count++;
            }
            else if(count == 1)
            {
                $(".popup").load("changeToFriends");
                count++;
            }
        }
    });
    $(document).on("click", "#next", () => {
        if(count == 0)
        {
            $(".popup").load("changeToBio");
            count++;
        }
        else if(count == 1)
        {
            $(".popup").load("changeToFriends");
            count++;
        }
    });
    $(document).on("click", "#back", () => {
        console.log(count);
        if(count == 1)
        {
            $(".popup").load("changeToPhotos");
            $("#photoDropdown").append("<option id='pFile' value='newFile'>" + photoName +"</option>");
            $('#photoDropdown').val("newFile");
            
            $("#backDropdown").append("<option id='bFile' value='newFile'>" + backName +"</option>");
            $('#backDropdown').val("newFile");
            count--;
        }
        else if(count == 2)
        {
            $(".popup").load("changeToBio");
            count--;
        }
    });
//  ___________________ photo upload _______________________//

    $(document).on("change", "#photoUpload", e => {
        photoName = e.target.files[0].name;
        $("#photoDropdown").append("<option id='pFile' value='newFile'>" + photoName +"</option>");
        $('#photoDropdown').val("newFile");
    });
    $(document).on("change", "#backgroundUpload", e => {
        backName = e.target.files[0].name;
        $("#backDropdown").append("<option id='bFile' value='newFile'>" + backName +"</option>");
        $('#backDropdown').val("newFile");
    });
    $('popupForm').submit(e => {
        e.preventDefault()
        $.ajax({
            url: '/checkedIn',
            method: 'post'
        });
        $(this).trigger('reset');
    });
// ___________If there's a popup don't allow scroll and make buttons change color__________ //
    if($(".popup").length < 1){}
    else
    {
        $("body").css("position", "fixed");
        $("body").css("overflow-y", "scroll");
        $("body").css("width", "100%");

        $("#popupButton").on("mouseenter", ()=> {
            $("#popupButton").css("background-color", "lightgray");
        }).on("mouseleave", () => {
            $("#popupButton").css("background-color", "white");
        });
    }


// _______ Buttons on main page __________  //

    $(document).on("mouseenter", "#comment", () => {
        $("#comment").css({fill: "blue"}).css("cursor", "pointer");
    }).on("mouseleave", "#comment", () => {
        $("#comment").css({fill: "black"});
    });

    $(document).on("mouseenter", "#heart", () => {
        $("#heart").css({fill: "blue"}).css("cursor", "pointer");
    }).on("mouseleave", "#heart", () => {
        $("#heart").css({fill: "black"});
    });

    $(document).on("mouseenter", "#bookmark", () => {
        $("#bookmark").css({fill: "blue"}).css("cursor", "pointer");
    }).on("mouseleave", "#bookmark", () => {
        $("#bookmark").css({fill: "black"});
    });

    $(document).on("mouseenter", "#arrow", () => {
        $("#arrow").css({fill: "blue"}).css("cursor", "pointer");
    }).on("mouseleave", "#arrow", () => {
        $("#arrow").css({fill: "black"});
    });
    $(document).on("mouseenter", "#threeDots", () => {
        $("#threeDots").css({fill: "blue"}).css("cursor", "pointer");
    }).on("mouseleave", "#threeDots", () => {
        $("#threeDots").css({fill: "black"});
    });
});