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
        if(count == 1)
        {
            $.ajax({
                url: "/changeToPhotos",
                method: "get",
                success: res => $(".popup").replaceWith(res),
                complete: () => {
                    if(photoName != "Default")
                    {
                        $("#photoDropdown").append("<option id='pFile' value='newFile'>" + photoName +"</option>");
                        $('#photoDropdown').val("newFile");
                    }
                    if(backName != "Default")
                    {
                        $("#backDropdown").append("<option id='bFile' value='newFile'>" + backName +"</option>");
                        $('#backDropdown').val("newFile");
                    }
                }
            });
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
//___________________ Finish ________________________//
    $(document).on("click", "#finish", () => {
        console.log("You should be on the friends page");
        $.ajax({
            url: "/processPopup",
            method: "post",
            success: () => {
                $(".popup").remove();
                $("body").css("position", "");
                $("body").css("overflow-y", "");
            }
        });
    });

// _______ Buttons on main page __________  //
    $(".feedFooter")
        .on("mouseenter", "svg", e => {
            $(e.target).css({fill: "blue", "cursor": "pointer"});
        })
        .on("mouseleave", "svg", e => {
            $(e.target).css({fill: "rgb(62,78,90)"})
        });
    $(document).on("click", "#threeDots", () => {
        
    })
// ________On click events for posts_______________//

    $(document).on("click", ".postInput", () => {
        $(".postPopup").css({"z-index": "2", "position": "absolute", "width": "85%", "height": "20rem"});
        $(".main").css({"-webkit-filter": "blur(5px)", "-moz-filter": "blur(5px)", "-o-filter": "blur(5px)", "-ms-filter": "blur(5px)", "filter": "blur(5px)"});
        $(".postPopup").load("addPostView");
    });
    $(document).on("click", "#postCancel", () => {
        $(".postPopup").empty().css({"width": "0", "height": "0"})
        $(".main").css({"-webkit-filter": "blur(0px)", "-moz-filter": "blur(0px)", "-o-filter": "blur(0px)", "-ms-filter": "blur(0px)", "filter": "blur(0px)"});
    });
    
    //_______ Actions ____________//
        // Like


    $(".actions").on("click", "svg", e => {
        if($(e.target).attr("id") != undefined)
        {
            if($(e.target).attr("id").includes('comment'))
            {
                let comId = Number($(e.target).attr("id").substring(7));
                $(".postPopup").css({"z-index": "2", "position": "absolute", "width": "85%", "height": "50%"});
                $(".main").css({"-webkit-filter": "blur(5px)", "-moz-filter": "blur(5px)", "-o-filter": "blur(5px)", "-ms-filter": "blur(5px)", "filter": "blur(5px)"});
                $.ajax({
                    url: '/addCommentView',
                    method: 'get',
                    data: {postId: comId},
                    success: res => {
                        $(".postPopup").append(res);
                    }
                });
            }
            else if($(e.target).attr("id").includes('arrow'))
            {   
                let arrId = Number($(e.target).attr("id").substring(5));
                console.log(arrId);
            }
            else if($(e.target).attr("id").includes('bookmark'))
            {
                let bookmarkId = Number($(e.target).attr("id").substring(8));
                console.log(bookmarkId);
            }
        }
        
    });
        // Comment

    $(document).on("click", "#commentCancel", () => {
        $(".postPopup").empty().css({"width": "0", "height": "0"})
        $(".main").css({"-webkit-filter": "blur(0px)", "-moz-filter": "blur(0px)", "-o-filter": "blur(0px)", "-ms-filter": "blur(0px)", "filter": "blur(0px)"});
    });
    // Links 
    $(document).on("click", "img", e => {
        if($(e.target).attr("id").includes('feedProfImg'))
        {
            console.log("You clicked me!");
            let userId = Number($(e.target).attr("id").substring(11));
            $.ajax({
                url: "/profile/" + userId,
                type: "get",
                success: res => {
                    window.history.pushState(userId, "LinkedCS", "/profile/" + userId);
                    document.open();
                    document.write(res);
                    document.close();
                }
            });
        }
    });
    // Adjust Settings
        // Pen
    $(".table2")
        .on("mouseenter", ".viewCountPen", () => {
            $("#penIcon").css({fill: "blue", "cursor": "pointer"});

        })
        .on("mouseleave", ".viewCountPen", () => {
            $("#penIcon").css({fill: "rgb(62,78,90)"});
        })
        .on("click", ".viewCountPen", () => {
            $.ajax({
                url: "/penPopup",
                type: "get",
                success: res => {
                    $(".viewerPref").append(res);
                }
            });
        });

    $(document).on("click", "html", e => {
        if($(e.target).closest('.penBody').length === 0) {
            document.getElementById("penPost") != undefined ? 
                $(".penBody").remove()
            :
                "";
        }
    });
    
    $(document)
        .on("change", ".penBody", e => {
            console.log("Change Event");
            let optVal = document.getElementById("penPost").value;
            let posArr = ["total", "year", "month", "week", "today"];
            posArr.includes(optVal) ? 
                $.ajax({
                    url: "/changeViewerPref",
                    type: "post",
                    data: {"viewPref": e.target.value},
                    success: () => {
                        $(".penBody").remove();
                    }
                })
            :
                console.log(`not valid, optVal is ${optVal}`);
        });
});

