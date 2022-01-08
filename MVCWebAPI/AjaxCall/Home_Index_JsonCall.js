
//page load event
$(document).ready(function () {
    //load user data on page loads
    loadUserData();

});


//add user data
function AddUserData() {

    var _this = $(this);
    var _form = $("#formProduct");
    var validator = _form.validate(); // obtain validator
    var anyError = false;
    _form.find("input,select,textarea").each(function () {
        if (!validator.element(this)) {
            // validate every input element inside this step
            anyError = true;
        }
    });


    var Form = document.getElementById("btnAdd").getAttribute("action");
    var formData = new FormData();
    var files = $("#uPhotoFile").get(0).files;

    formData.append("userName", uName.value);
    formData.append("userEmail", uEmail.value);
    formData.append("userImage", files[0]);


    $.ajax({
        url: "/api/UserAPI",
        type: "POST",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (successResponse) {

            //return 2 if no photo selected
            if (successResponse == 2) {
                alert("Please Select Image");
            }
            else {


                //close model after save
                $('#myModal').modal("hide");

                alert("Product Added Succesfully");

                location.reload();



            }


        },
        error: function (errorResponse) {
            console.log("Error:", errorResponse.responseText);
        }
    });
}


//load user data on page load 
function loadUserData() {

    $.ajax({
        type: "Get",
        url: "/api/UserAPI",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (retData) {

            var html = '';
            $.each(retData, function (key, item) {

                html += '<tr>';
                html += '<td>' + item.uID + '</td>';
                html += '<td>' + item.uName + '</td>';
                html += '<td>' + item.uEmail + '</td>';
                html += '<td><img src="' + item.uPhoto.replace("~", "") + '" height="100" width="100"></td>';
                html += '<td><a href="#" onclick="getDataById(' + item.uID + ');">Edit</a> | <a href="#" onclick="DeleteData(' + item.uID + ');">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);

        },
        error: function (errorResponse) {
            console.log("Error:", errorResponse.responseText);
        }
    });

}


//delete data
function DeleteData(userID) {

    debugger;

    $.ajax({
        type: "DELETE",
        url: "/api/UserAPI/" + userID,
        //data: { id : userID },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (retData) {

            if (retData == 1) {
                loadUserData();
                alert("Data Deleted succesfully");
            }
            else {
                alert("Error Accured");
            }

        },
        error: function (errorResponse) {
            console.log("Error:", errorResponse.responseText);
        }
    });
}

//get data by id for update
function getDataById(userID) {

    $.ajax({
        url: "/api/UserAPI",
        data: { id: userID },
        type: "Get",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (retData) {



            //put data in textbox
            $("#uID").val(retData.uID);
            $("#uName").val(retData.uName);
            $("#uEmail").val(retData.uEmail);
            $("#UserImage").attr('src', retData.uPhoto.replace("~", ""));

            //store data in hidden field
            $("#UserImageHiddenId").val(retData.uPhoto);

            //show model
            $('#myModal').modal("show");

            //hide add button and show update button
            $("#btnAdd").hide();
            $("#btnUpdate").show();


        },
        error: function (errorResponse) {
            console.log("Error:", errorResponse.responseText);
        }
    });
}


//update user data on click event
function UpdateData() {

    var _this = $(this);
    var _form = $("#formProduct");
    var validator = _form.validate(); // obtain validator
    var anyError = false;
    _form.find("input,select,textarea").each(function () {
        if (!validator.element(this)) {
            // validate every input element inside this step
            anyError = true;
        }
    });

    var Form = document.getElementById("btnUpdate").getAttribute("action");
    var formData = new FormData();
    var files = $("#uPhotoFile").get(0).files;

    formData.append("userID", uID.value);
    formData.append("userName", uName.value);
    formData.append("userEmail", uEmail.value);
    formData.append("userImage", files[0]);
    formData.append("UserImageHidden", UserImageHiddenId.value);


    $.ajax({
        type: "PUT",
        url: "/api/UserAPI",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (retData) {

            debugger;

            if (retData == 1) {

                //hide model
                $('#myModal').modal("hide");

                //load user data after update
                loadUserData();

                alert("Data updated Successfully");



            }
            else {
                alert("Error");
            }


        },
        error: function (errorResponse) {
            console.log("Error:", errorResponse.responseText);
        }
    });
}


//clear text box
function ClearTextBox() {
    debugger;
    $("#uID").val("");
    $("#uName").val("");
    $("#uEmail").val("");
    $("#UserImage").attr('src', "");


    $("#btnAdd").show();
    $("#btnUpdate").hide();
}