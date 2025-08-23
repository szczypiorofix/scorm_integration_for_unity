var ScormAPILibrary = {
    ScormInitialize: function() {
        if (!pipwerks.SCORM) {
            console.error("SCORM wrapper (pipwerks.SCORM) not found! Check script load order in index.html.");
            return;
        }

        console.log("SCORM wrapper found. Initializing...");
        return pipwerks.SCORM.init();
    },

    ScormSetValue: function(element, value) {
        if (!pipwerks.SCORM) {
            console.error("SCORM wrapper (pipwerks.SCORM) not found! Check script load order in index.html.");
            return;
        }

        var elementStr = UTF8ToString(element);
        var valueStr = UTF8ToString(value);
        console.log("SCORM SetValue:", elementStr, valueStr);
        pipwerks.SCORM.set(elementStr, valueStr);
    },

    ScormCommit: function() {
        if (!pipwerks.SCORM) {
            console.error("SCORM wrapper (pipwerks.SCORM) not found! Check script load order in index.html.");
            return;
        }
        
        console.log("SCORM Commit");
        pipwerks.SCORM.save();
    },

    ScormFinish: function() {
        if (!pipwerks.SCORM) {
            console.error("SCORM wrapper (pipwerks.SCORM) not found! Check script load order in index.html.");
            return;
        }
        
        console.log("SCORM Finish");
        pipwerks.SCORM.quit();
    }
};

mergeInto(LibraryManager.library, ScormAPILibrary);
