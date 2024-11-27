// vite.config.ts
import { fileURLToPath, URL } from "node:url";
import { defineConfig } from "file:///C:/Users/Damia/Dropbox/MyLifeInAFolder/AllRepositorys/2/ProductManagement/productmanagement.client/node_modules/vite/dist/node/index.js";
import plugin from "file:///C:/Users/Damia/Dropbox/MyLifeInAFolder/AllRepositorys/2/ProductManagement/productmanagement.client/node_modules/@vitejs/plugin-react/dist/index.mjs";
import fs from "fs";
import path from "path";
import child_process from "child_process";
import { env } from "process";
var __vite_injected_original_import_meta_url = "file:///C:/Users/Damia/Dropbox/MyLifeInAFolder/AllRepositorys/2/ProductManagement/productmanagement.client/vite.config.ts";
var baseFolder = env.APPDATA !== void 0 && env.APPDATA !== "" ? `${env.APPDATA}/ASP.NET/https` : `${env.HOME}/.aspnet/https`;
var certificateName = "productmanagement.client";
var certFilePath = path.join(baseFolder, `${certificateName}.pem`);
var keyFilePath = path.join(baseFolder, `${certificateName}.key`);
if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
  if (0 !== child_process.spawnSync("dotnet", [
    "dev-certs",
    "https",
    "--export-path",
    certFilePath,
    "--format",
    "Pem",
    "--no-password"
  ], { stdio: "inherit" }).status) {
    throw new Error("Could not create certificate.");
  }
}
var target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` : env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(";")[0] : "https://localhost:7204";
var vite_config_default = defineConfig({
  plugins: [plugin()],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", __vite_injected_original_import_meta_url))
    }
  },
  server: {
    proxy: {
      "^/products": {
        target,
        secure: false
      },
      "^/categories": {
        target,
        secure: false
      }
    },
    //port: 5173,
    port: 7204,
    https: {
      key: fs.readFileSync(keyFilePath),
      cert: fs.readFileSync(certFilePath)
    }
  }
});
export {
  vite_config_default as default
};
//# sourceMappingURL=data:application/json;base64,ewogICJ2ZXJzaW9uIjogMywKICAic291cmNlcyI6IFsidml0ZS5jb25maWcudHMiXSwKICAic291cmNlc0NvbnRlbnQiOiBbImNvbnN0IF9fdml0ZV9pbmplY3RlZF9vcmlnaW5hbF9kaXJuYW1lID0gXCJDOlxcXFxVc2Vyc1xcXFxEYW1pYVxcXFxEcm9wYm94XFxcXE15TGlmZUluQUZvbGRlclxcXFxBbGxSZXBvc2l0b3J5c1xcXFwyXFxcXFByb2R1Y3RNYW5hZ2VtZW50XFxcXHByb2R1Y3RtYW5hZ2VtZW50LmNsaWVudFwiO2NvbnN0IF9fdml0ZV9pbmplY3RlZF9vcmlnaW5hbF9maWxlbmFtZSA9IFwiQzpcXFxcVXNlcnNcXFxcRGFtaWFcXFxcRHJvcGJveFxcXFxNeUxpZmVJbkFGb2xkZXJcXFxcQWxsUmVwb3NpdG9yeXNcXFxcMlxcXFxQcm9kdWN0TWFuYWdlbWVudFxcXFxwcm9kdWN0bWFuYWdlbWVudC5jbGllbnRcXFxcdml0ZS5jb25maWcudHNcIjtjb25zdCBfX3ZpdGVfaW5qZWN0ZWRfb3JpZ2luYWxfaW1wb3J0X21ldGFfdXJsID0gXCJmaWxlOi8vL0M6L1VzZXJzL0RhbWlhL0Ryb3Bib3gvTXlMaWZlSW5BRm9sZGVyL0FsbFJlcG9zaXRvcnlzLzIvUHJvZHVjdE1hbmFnZW1lbnQvcHJvZHVjdG1hbmFnZW1lbnQuY2xpZW50L3ZpdGUuY29uZmlnLnRzXCI7aW1wb3J0IHsgZmlsZVVSTFRvUGF0aCwgVVJMIH0gZnJvbSAnbm9kZTp1cmwnO1xyXG5cclxuaW1wb3J0IHsgZGVmaW5lQ29uZmlnIH0gZnJvbSAndml0ZSc7XHJcbmltcG9ydCBwbHVnaW4gZnJvbSAnQHZpdGVqcy9wbHVnaW4tcmVhY3QnO1xyXG5pbXBvcnQgZnMgZnJvbSAnZnMnO1xyXG5pbXBvcnQgcGF0aCBmcm9tICdwYXRoJztcclxuaW1wb3J0IGNoaWxkX3Byb2Nlc3MgZnJvbSAnY2hpbGRfcHJvY2Vzcyc7XHJcbmltcG9ydCB7IGVudiB9IGZyb20gJ3Byb2Nlc3MnO1xyXG5cclxuY29uc3QgYmFzZUZvbGRlciA9XHJcbiAgICBlbnYuQVBQREFUQSAhPT0gdW5kZWZpbmVkICYmIGVudi5BUFBEQVRBICE9PSAnJ1xyXG4gICAgICAgID8gYCR7ZW52LkFQUERBVEF9L0FTUC5ORVQvaHR0cHNgXHJcbiAgICAgICAgOiBgJHtlbnYuSE9NRX0vLmFzcG5ldC9odHRwc2A7XHJcblxyXG5jb25zdCBjZXJ0aWZpY2F0ZU5hbWUgPSBcInByb2R1Y3RtYW5hZ2VtZW50LmNsaWVudFwiO1xyXG5jb25zdCBjZXJ0RmlsZVBhdGggPSBwYXRoLmpvaW4oYmFzZUZvbGRlciwgYCR7Y2VydGlmaWNhdGVOYW1lfS5wZW1gKTtcclxuY29uc3Qga2V5RmlsZVBhdGggPSBwYXRoLmpvaW4oYmFzZUZvbGRlciwgYCR7Y2VydGlmaWNhdGVOYW1lfS5rZXlgKTtcclxuXHJcbmlmICghZnMuZXhpc3RzU3luYyhjZXJ0RmlsZVBhdGgpIHx8ICFmcy5leGlzdHNTeW5jKGtleUZpbGVQYXRoKSkge1xyXG4gICAgaWYgKDAgIT09IGNoaWxkX3Byb2Nlc3Muc3Bhd25TeW5jKCdkb3RuZXQnLCBbXHJcbiAgICAgICAgJ2Rldi1jZXJ0cycsXHJcbiAgICAgICAgJ2h0dHBzJyxcclxuICAgICAgICAnLS1leHBvcnQtcGF0aCcsXHJcbiAgICAgICAgY2VydEZpbGVQYXRoLFxyXG4gICAgICAgICctLWZvcm1hdCcsXHJcbiAgICAgICAgJ1BlbScsXHJcbiAgICAgICAgJy0tbm8tcGFzc3dvcmQnLFxyXG4gICAgXSwgeyBzdGRpbzogJ2luaGVyaXQnLCB9KS5zdGF0dXMpIHtcclxuICAgICAgICB0aHJvdyBuZXcgRXJyb3IoXCJDb3VsZCBub3QgY3JlYXRlIGNlcnRpZmljYXRlLlwiKTtcclxuICAgIH1cclxufVxyXG5cclxuY29uc3QgdGFyZ2V0ID0gZW52LkFTUE5FVENPUkVfSFRUUFNfUE9SVCA/IGBodHRwczovL2xvY2FsaG9zdDoke2Vudi5BU1BORVRDT1JFX0hUVFBTX1BPUlR9YCA6XHJcbiAgICBlbnYuQVNQTkVUQ09SRV9VUkxTID8gZW52LkFTUE5FVENPUkVfVVJMUy5zcGxpdCgnOycpWzBdIDogJ2h0dHBzOi8vbG9jYWxob3N0OjcyMDQnO1xyXG5cclxuLy8gaHR0cHM6Ly92aXRlanMuZGV2L2NvbmZpZy9cclxuZXhwb3J0IGRlZmF1bHQgZGVmaW5lQ29uZmlnKHtcclxuICAgIHBsdWdpbnM6IFtwbHVnaW4oKV0sXHJcbiAgICByZXNvbHZlOiB7XHJcbiAgICAgICAgYWxpYXM6IHtcclxuICAgICAgICAgICAgJ0AnOiBmaWxlVVJMVG9QYXRoKG5ldyBVUkwoJy4vc3JjJywgaW1wb3J0Lm1ldGEudXJsKSlcclxuICAgICAgICB9XHJcbiAgICB9LFxyXG4gICAgc2VydmVyOiB7XHJcbiAgICAgICAgcHJveHk6IHtcclxuICAgICAgICAgICAgJ14vcHJvZHVjdHMnOiB7XHJcbiAgICAgICAgICAgICAgICB0YXJnZXQsXHJcbiAgICAgICAgICAgICAgICBzZWN1cmU6IGZhbHNlXHJcbiAgICAgICAgICAgIH0sXHJcbiAgICAgICAgICAgICdeL2NhdGVnb3JpZXMnOiB7XHJcbiAgICAgICAgICAgICAgICB0YXJnZXQsXHJcbiAgICAgICAgICAgICAgICBzZWN1cmU6IGZhbHNlXHJcbiAgICAgICAgICAgIH1cclxuICAgICAgICB9LFxyXG4gICAgICAgIC8vcG9ydDogNTE3MyxcclxuICAgICAgICBwb3J0OiA3MjA0LFxyXG4gICAgICAgIGh0dHBzOiB7XHJcbiAgICAgICAgICAgIGtleTogZnMucmVhZEZpbGVTeW5jKGtleUZpbGVQYXRoKSxcclxuICAgICAgICAgICAgY2VydDogZnMucmVhZEZpbGVTeW5jKGNlcnRGaWxlUGF0aCksXHJcbiAgICAgICAgfVxyXG4gICAgfVxyXG59KVxyXG4iXSwKICAibWFwcGluZ3MiOiAiO0FBQTBlLFNBQVMsZUFBZSxXQUFXO0FBRTdnQixTQUFTLG9CQUFvQjtBQUM3QixPQUFPLFlBQVk7QUFDbkIsT0FBTyxRQUFRO0FBQ2YsT0FBTyxVQUFVO0FBQ2pCLE9BQU8sbUJBQW1CO0FBQzFCLFNBQVMsV0FBVztBQVB5UyxJQUFNLDJDQUEyQztBQVM5VyxJQUFNLGFBQ0YsSUFBSSxZQUFZLFVBQWEsSUFBSSxZQUFZLEtBQ3ZDLEdBQUcsSUFBSSxPQUFPLG1CQUNkLEdBQUcsSUFBSSxJQUFJO0FBRXJCLElBQU0sa0JBQWtCO0FBQ3hCLElBQU0sZUFBZSxLQUFLLEtBQUssWUFBWSxHQUFHLGVBQWUsTUFBTTtBQUNuRSxJQUFNLGNBQWMsS0FBSyxLQUFLLFlBQVksR0FBRyxlQUFlLE1BQU07QUFFbEUsSUFBSSxDQUFDLEdBQUcsV0FBVyxZQUFZLEtBQUssQ0FBQyxHQUFHLFdBQVcsV0FBVyxHQUFHO0FBQzdELE1BQUksTUFBTSxjQUFjLFVBQVUsVUFBVTtBQUFBLElBQ3hDO0FBQUEsSUFDQTtBQUFBLElBQ0E7QUFBQSxJQUNBO0FBQUEsSUFDQTtBQUFBLElBQ0E7QUFBQSxJQUNBO0FBQUEsRUFDSixHQUFHLEVBQUUsT0FBTyxVQUFXLENBQUMsRUFBRSxRQUFRO0FBQzlCLFVBQU0sSUFBSSxNQUFNLCtCQUErQjtBQUFBLEVBQ25EO0FBQ0o7QUFFQSxJQUFNLFNBQVMsSUFBSSx3QkFBd0IscUJBQXFCLElBQUkscUJBQXFCLEtBQ3JGLElBQUksa0JBQWtCLElBQUksZ0JBQWdCLE1BQU0sR0FBRyxFQUFFLENBQUMsSUFBSTtBQUc5RCxJQUFPLHNCQUFRLGFBQWE7QUFBQSxFQUN4QixTQUFTLENBQUMsT0FBTyxDQUFDO0FBQUEsRUFDbEIsU0FBUztBQUFBLElBQ0wsT0FBTztBQUFBLE1BQ0gsS0FBSyxjQUFjLElBQUksSUFBSSxTQUFTLHdDQUFlLENBQUM7QUFBQSxJQUN4RDtBQUFBLEVBQ0o7QUFBQSxFQUNBLFFBQVE7QUFBQSxJQUNKLE9BQU87QUFBQSxNQUNILGNBQWM7QUFBQSxRQUNWO0FBQUEsUUFDQSxRQUFRO0FBQUEsTUFDWjtBQUFBLE1BQ0EsZ0JBQWdCO0FBQUEsUUFDWjtBQUFBLFFBQ0EsUUFBUTtBQUFBLE1BQ1o7QUFBQSxJQUNKO0FBQUE7QUFBQSxJQUVBLE1BQU07QUFBQSxJQUNOLE9BQU87QUFBQSxNQUNILEtBQUssR0FBRyxhQUFhLFdBQVc7QUFBQSxNQUNoQyxNQUFNLEdBQUcsYUFBYSxZQUFZO0FBQUEsSUFDdEM7QUFBQSxFQUNKO0FBQ0osQ0FBQzsiLAogICJuYW1lcyI6IFtdCn0K
