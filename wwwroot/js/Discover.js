$(document).ready(() => {
    $(document).on("click", "#searchButton", () => {
        $("#searchButton").replaceWith("<input type='text' id='searchInput'>");
    });
    var inputVal = "";
    var iv = $("#searchInput").val();
    $(document).on("change paste keyup", iv, () => {
        $(".mainContent").empty();



        inputVal = $("#searchInput").val();
        $.ajax({
            url: '/searchPeople',
            method: 'post',
            data: {inputVal},
            success: res => {
                // returns a list of user objects
                console.log(res);
                for(let i=0;i<res.length;i++)
                {
                    if(res[i].firstName != undefined)
                    {
                        let response = res[i];
                        $.ajax({
                            url: '/showPerson',
                            method: 'post',
                            data: {user: response},
                            success: res => {
                                $(".mainContent").append(res);
                            }
                        });
                    }
                }
            }
        });
    });
});