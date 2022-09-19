app.directive("limitToNumber", [function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            var limit = parseInt(attrs.limitToNumber);
            angular.element(elem).on("keypress", function (event) {
                if (event.keyCode >= 48 && event.keyCode <= 57) {
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
