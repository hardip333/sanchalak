app.directive("maxLength", [function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            var limit = parseInt(attrs.limitToNo);
            angular.element(elem).on("keypress", function (event) {
                if (this.value.length >= limit)
                    return false;
            });
        }
    }
}]);