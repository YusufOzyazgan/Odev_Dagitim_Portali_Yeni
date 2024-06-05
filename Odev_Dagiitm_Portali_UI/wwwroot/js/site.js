// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
function showAdminPageIfAdmin(userRoles) {
    
    if (userRoles.includes('Admin')) {
        $(".AdminPage").show(); 
    } else {
        $(".AdminPage").hide();
    }
}
function showTeacherPageIfTeacher(userRoles) {
    
    if (userRoles.includes('Teacher')) {
        $(".TeacherPage").show(); 
    } else {
        $(".TeacherPage").hide(); 
    }
}
function showStudentPageIfStudent(userRoles) {

    if (userRoles.includes('Student')) {
        $(".StudentPage").show();
    } else {
        $(".StudentPage").hide();
    }
}
function showUserClass() {
   
    console.log(apiUrl + 'api/Class/GetUserClasses' + userId)
    $.ajax({
        url: apiUrl + 'api/Class/GetUserClasses/' + userId,
        type: 'GET',
        data: { userId: userId },
        headers: {
            "Authorization": "Bearer " + token
        },
        success: function (classes) {
            var userClasses = classes; // Veriyi global değişkene atayın
            console.log(userId)
            
            var $registeredClassesList = $('#registeredClassesList');
            var $registeredClassesList2 = $('#registeredClassesList2');
            var classSelect = $('#addHomeworkTeacherClasses');
            $registeredClassesList.empty();
            $registeredClassesList2.empty();
            $.each(userClasses, function (index, classes) {
                $registeredClassesList.append('<li class="list-group-item mb-1 text-center" data-id="' + classes.id + '"><b>' + classes.className + '&nbsp;&nbsp;</b><button class="btn btn-outline-danger btn-sm btnDelete">Sil</button></li>');
            });
            $.each(userClasses, function (index, classes2) {
                $registeredClassesList2.append('<li class="list-group-item mb-1 text-center" data-id="' + classes2.id + '"><b>' + classes2.className + '&nbsp;&nbsp;</b></li>');
            });
            
            console.log(userClasses)
            userClasses.forEach(function (userClass) {
                classSelect.append('<option value="' + userClass.classId + '">' + userClass.className + '</option>');
            });
        },
        error: function (error) {
            console.log('Error fetching user classes:', error);
        }
    });
}
var token = localStorage.getItem("token")
var userRoles = [];
var apiUrl = "https://localhost:7106/";
var userId = ";"

if (token == null) {

    $(".NotLogined").show();
    $(".Logined").hide();

} else {
   
    $(".NotLogined").hide();
    $(".Logined").show();
    var payload = parseJwt(token);
    var username = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    userRoles = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    
    userId = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    $("#UserName").html(username);
    showAdminPageIfAdmin(userRoles);
    showStudentPageIfStudent(userRoles);
    showTeacherPageIfTeacher(userRoles);
    showUserClass();
}



function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);

}
$("#Logout").click(function () {
    localStorage.removeItem("token");
    location.href = "/Home/Login";
});
// Write your JavaScript code.
