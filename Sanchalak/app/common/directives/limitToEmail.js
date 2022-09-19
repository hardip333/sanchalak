app.directive("limitToEmail", [function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            var limit = parseInt(attrs.limitToEmail);
            angular.element(elem).on("keypress", function (event) {
                if ((event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122) || (event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 20 || event.keyCode == 8 || event.keyCode == 17 || event.keyCode == 46 || event.keyCode == 13 || event.keyCode == 27 || event.keyCode == 16 || event.keyCode == 32 || event.keyCode == 9 || event.keyCode == 45 || event.keyCode == 64) {
                    if (this.value.length >= limit) {
                        return false;
                    }
                }
                else
                    return false;
            });
        }
    }
}]);
